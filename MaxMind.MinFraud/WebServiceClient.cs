#region

using MaxMind.MinFraud.Exception;
using MaxMind.MinFraud.Request;
using MaxMind.MinFraud.Response;
using MaxMind.MinFraud.Util;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#endregion

namespace MaxMind.MinFraud
{
    /// <summary>
    /// Client for querying the minFraud Score and Insights web services.
    /// </summary>
    public sealed class WebServiceClient : IDisposable, IWebServiceClient
    // If removing sealed, update Dispose methods.
    {
        private static readonly string Version =
            ((AssemblyInformationalVersionAttribute?)
                typeof(WebServiceClient).GetTypeInfo().Assembly.GetCustomAttribute(
                    typeof(AssemblyInformationalVersionAttribute)))?.InformationalVersion ?? "unknown";

        private const string BasePath = "/minfraud/v2.0/";
        private readonly HttpClient _httpClient;
        private readonly List<string> _locales;
        private bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebServiceClient" /> class.
        /// </summary>
        /// <param name="httpClient">Injected HttpClient.</param>
        /// <param name="options">Injected Options.</param>
        [CLSCompliant(false)]
        public WebServiceClient(
            HttpClient httpClient,
            IOptions<WebServiceClientOptions> options
        ) : this(
            options.Value.AccountId,
            options.Value.LicenseKey,
            options.Value.Locales,
            options.Value.Host,
            options.Value.Timeout,
            httpClient)
        {
        }

        /// <summary>
        /// Constructor for minFraud web service client.
        /// </summary>
        /// <param name="accountId">Your MaxMind account ID.</param>
        /// <param name="licenseKey">Your MaxMind license key.</param>
        /// <param name="locales">A list of locale codes to use for name property.</param>
        /// <param name="host">The host to use when connecting to the web service.</param>
        /// <param name="timeout">The timeout to use for the request.</param>
        /// <param name="httpMessageHandler">Handler to use in request. The handler will be disposed.</param>
        public WebServiceClient(
            int accountId,
            string licenseKey,
            IEnumerable<string>? locales = null,
            string host = "minfraud.maxmind.com",
            TimeSpan? timeout = null,
            HttpMessageHandler? httpMessageHandler = null
            ) : this(
                accountId,
                licenseKey,
                locales,
                host,
                timeout,
                new HttpClient(httpMessageHandler ?? new HttpClientHandler(), true))
        {
        }

        internal WebServiceClient(
         int accountId,
         string licenseKey,
         IEnumerable<string>? locales,
         string host,
         TimeSpan? timeout,
         HttpClient httpClient
         )
        {
            _locales = locales == null ? new List<string> { "en" } : new List<string>(locales);

            httpClient.BaseAddress = new UriBuilder("https", host, -1, BasePath).Uri;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{accountId}:{licenseKey}")
                    )
                );
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("minFraud-api-dotnet", Version));

            if (timeout != null)
            {
                httpClient.Timeout = (TimeSpan)timeout;
            }

            _httpClient = httpClient;
        }

        /// <summary>
        /// Asynchronously query Factors endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud
        /// Factors response data</returns>
        public async Task<Factors> FactorsAsync(Transaction transaction)
        {
            var factors = await MakeResponse<Factors>(transaction).ConfigureAwait(false);
            factors.IPAddress.SetLocales(_locales);
            return factors;
        }

        /// <summary>
        /// Asynchronously query Insights endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud
        /// Insights response data</returns>
        public async Task<Insights> InsightsAsync(Transaction transaction)
        {
            var insights = await MakeResponse<Insights>(transaction).ConfigureAwait(false);
            insights.IPAddress.SetLocales(_locales);
            return insights;
        }

        /// <summary>
        /// Asynchronously query Score endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud Score
        /// response data</returns>
        public async Task<Score> ScoreAsync(Transaction transaction)
        {
            return await MakeResponse<Score>(transaction).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously query the minFraud Report Transaction API.
        /// </summary>
        /// <remarks>
        /// Reporting transactions to MaxMind helps us detect about 10-50% more
        /// fraud and reduce false positives for you.
        /// </remarks>
        /// <param name="report">The transaction report you would like to send.</param>
        /// <returns>The Task on which to await. The web service returns no data and
        /// this API will throw an exception if there is an error.</returns>
        public async Task ReportAsync(TransactionReport report)
        {
            await MakeRequest("transactions/report", report);
        }

        private async Task<T> MakeResponse<T>(Transaction request) where T : Score
        {
            var requestPath = typeof(T).Name.ToLower();
            var response = await MakeRequest(requestPath, request).ConfigureAwait(false);
            return await HandleSuccess<T>(response).ConfigureAwait(false);
        }

        private async Task<HttpResponseMessage> MakeRequest<T>(string path, T request)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var response = await _httpClient.PostAsJsonAsync<T>(
                path,
                request,
                options).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response).ConfigureAwait(false);
            }
            return response;
        }

        private static async Task<T> HandleSuccess<T>(HttpResponseMessage response) where T : Score
        {
            var contentType = response.Content.Headers.GetValues("Content-Type")?.FirstOrDefault();
            if (contentType == null || !contentType.Contains("json"))
            {
                throw new MinFraudException(
                    $"Received a {(int)response.StatusCode} response for {typeof(T).Name} but it does not appear to be JSON: {contentType}");
            }
            var options = new JsonSerializerOptions();
            options.Converters.Add(new NetworkConverter());

            try
            {
                var model = await response.Content.ReadFromJsonAsync<T>(options).ConfigureAwait(false);
                if (model == null)
                {
                    throw new HttpException(
                        $"Received a {(int)response.StatusCode} response for minFraud {typeof(T).Name} but there was no message body",
                        response.StatusCode, response.RequestMessage?.RequestUri);
                }
                return model;
            }
            catch (JsonException ex)
            {
                throw new MinFraudException(
                    $"Received a {(int)response.StatusCode} response but could not decode it as JSON", ex);
            }
        }

        private static async Task HandleError(HttpResponseMessage response)
        {
            var uri = response.RequestMessage?.RequestUri;
            var status = (int)response.StatusCode;

            switch (status)
            {
                case >= 400 and < 500:
                    await Handle4xxStatus(response).ConfigureAwait(false);
                    break;
                case >= 500 and < 600:
                    throw new HttpException(
                        $"Received a server ({status}) error for {uri}", response.StatusCode,
                        uri);
            }

            var errorMessage =
                $"Received an unexpected HTTP status ({status}) for {uri}";
            throw new HttpException(errorMessage, response.StatusCode, uri);
        }

        private static async Task Handle4xxStatus(HttpResponseMessage response)
        {
            var uri = response.RequestMessage?.RequestUri;
            var status = (int)response.StatusCode;

            // The null guard is primarily because our unit testing mock library does not
            // set Content for the default response.
            if (response.Content == null)
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} with no body",
                    response.StatusCode, uri);
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(content))
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} with no body",
                    response.StatusCode, uri);
            }

            try
            {
                var error = JsonSerializer.Deserialize<WebServiceError>(content);

                if (error == null)
                {
                    throw new HttpException(
                        $"Received a {status} error for {uri} but it did not include the expected JSON body: {content}",
                        response.StatusCode, uri);
                }
                HandleErrorWithJsonBody(error, response, content);
            }
            catch (JsonException ex)
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} but there was an error parsing it as JSON: {content}",
                    response.StatusCode, uri, ex);
            }
        }

        private static void HandleErrorWithJsonBody(WebServiceError error, HttpResponseMessage response,
            string content)
        {
            if (error.Code == null || error.Error == null)
            {
                throw new HttpException(
                    $"Error response contains JSON but it does not specify code or error keys: {content}",
                    response.StatusCode,
                    response.RequestMessage?.RequestUri);
            }
            throw error.Code switch
            {
                "ACCOUNT_ID_REQUIRED" or "AUTHORIZATION_INVALID" or "LICENSE_KEY_REQUIRED"
                    => new AuthenticationException(error.Error),
                "INSUFFICIENT_FUNDS" => new InsufficientFundsException(error.Error),
                "PERMISSION_REQUIRED" => new PermissionRequiredException(error.Error),
                _ => new InvalidRequestException(error.Error, error.Code, response.RequestMessage?.RequestUri),
            };
        }

        /// <summary>
        /// Dispose of the underlying HttpClient.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the underlying HttpClient.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _httpClient.Dispose();
            }

            _disposed = true;
        }
    }
}
