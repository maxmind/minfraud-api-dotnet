#region

using MaxMind.MinFraud.Exception;
using MaxMind.MinFraud.Request;
using MaxMind.MinFraud.Response;
using MaxMind.MinFraud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace MaxMind.MinFraud
{
    /// <summary>
    /// Client for querying the minFraud Score and Insights web services.
    /// </summary>
    public sealed class WebServiceClient : IDisposable
    // If removing sealed, update Dispose methods.
    {
        private static readonly string Version =
            ((AssemblyInformationalVersionAttribute)
                typeof(WebServiceClient).GetTypeInfo().Assembly.GetCustomAttribute(
                    typeof(AssemblyInformationalVersionAttribute))).InformationalVersion;

        private const string BasePath = "/minfraud/v2.0/";
        private readonly HttpClient _httpClient;
        private readonly List<string> _locales;
        private bool _disposed;
        private readonly HttpMessageHandler _httpMessageHandler;

        /// <summary>
        /// Constructor for minFraud web service client.
        /// </summary>
        /// <param name="accountId">Your MaxMind account ID.</param>
        /// <param name="licenseKey">Your MaxMind license key.</param>
        /// <param name="locales">A list of locale codes to use for name property.</param>
        /// <param name="host">The host to use when connecting to the web service.</param>
        /// <param name="timeout">The timeout to use for the request.</param>
        /// <param name="httpMessageHandler">Handler to use in request.</param>
        public WebServiceClient(
            int accountId,
            string licenseKey,
            IEnumerable<string>? locales = null,
            string host = "minfraud.maxmind.com",
            TimeSpan? timeout = null,
            HttpMessageHandler? httpMessageHandler = null
            )
        {
            _locales = locales == null ? new List<string> { "en" } : new List<string>(locales);
            _httpMessageHandler = httpMessageHandler ?? new HttpClientHandler();
            try
            {
                _httpClient = new HttpClient(httpMessageHandler ?? new HttpClientHandler())
                {
                    BaseAddress = new UriBuilder("https", host, -1, BasePath).Uri,
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(
                                Encoding.ASCII.GetBytes(
                                    $"{accountId}:{licenseKey}"))),
                        Accept = {new MediaTypeWithQualityHeaderValue("application/json")},
                        UserAgent = {new ProductInfoHeaderValue("minFraud-api-dotnet", Version)}
                    }
                };
                if (timeout != null)
                {
                    _httpClient.Timeout = timeout.Value;
                }
            }
            catch
            {
                _httpClient?.Dispose();
                _httpMessageHandler.Dispose();
                throw;
            }
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
        /// fraud and reduce false positives for you. We offer two ways to
        /// report transactions, through an online form or via an API that is
        /// documented on this page.
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

        private async Task<HttpResponseMessage> MakeRequest(string path, Object request)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new NetworkConverter());
            settings.Converters.Add(new StringEnumConverter());
            var requestBody = JsonConvert.SerializeObject(request, settings);

            var response = await _httpClient.PostAsync(path,
                new StringContent(requestBody, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

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
            using var s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var sr = new StreamReader(s);
            using JsonReader reader = new JsonTextReader(sr);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NetworkConverter());
            var serializer = JsonSerializer.Create(settings);
            try
            {
                var model = serializer.Deserialize<T>(reader);
                if (model == null)
                {
                    throw new HttpException(
                        $"Received a {(int)response.StatusCode} response for minFraud {typeof(T).Name} but there was no message body",
                        response.StatusCode, response.RequestMessage.RequestUri);
                }
                return model;
            }
            catch (JsonSerializationException ex)
            {
                throw new MinFraudException(
                    $"Received a {(int)response.StatusCode} response but could not decode it as JSON", ex);
            }
        }

        private static async Task HandleError(HttpResponseMessage response)
        {
            var uri = response.RequestMessage.RequestUri;
            var status = (int)response.StatusCode;

            if (status >= 400 && status < 500)
            {
                await Handle4xxStatus(response).ConfigureAwait(false);
            }
            else if (status >= 500 && status < 600)
            {
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
            var uri = response.RequestMessage.RequestUri;
            var status = (int)response.StatusCode;

            // The null guard is primarily because our unit testing mock library does not
            // set Content for the default response.
            var content = response.Content != null ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null;

            if (string.IsNullOrEmpty(content))
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} with no body",
                    response.StatusCode, uri);
            }

            try
            {
                var error = JsonConvert.DeserializeObject<WebServiceError>(content!);

                HandleErrorWithJsonBody(error, response, content!);
            }
            catch (JsonReaderException ex)
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} but it did not include the expected JSON body: {content}",
                    response.StatusCode, uri, ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new HttpException(
                    $"Error response contains JSON but it does not specify code or error keys: {content}",
                    response.StatusCode,
                    uri, ex);
            }
        }

        private static void HandleErrorWithJsonBody(WebServiceError error, HttpResponseMessage response,
            string content)
        {
            if (error.Code == null || error.Error == null)
                throw new HttpException(
                    $"Error response contains JSON but it does not specify code or error keys: {content}",
                    response.StatusCode,
                    response.RequestMessage.RequestUri);
            switch (error.Code)
            {
                case "ACCOUNT_ID_REQUIRED":
                case "AUTHORIZATION_INVALID":
                case "LICENSE_KEY_REQUIRED":
                case "USER_ID_REQUIRED":
                    throw new AuthenticationException(error.Error);
                case "INSUFFICIENT_FUNDS":
                    throw new InsufficientFundsException(error.Error);
                case "PERMISSION_REQUIRED":
                    throw new PermissionRequiredException(error.Error);
                default:
                    throw new InvalidRequestException(error.Error, error.Code, response.RequestMessage.RequestUri);
            }
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
                _httpMessageHandler.Dispose();
            }

            _disposed = true;
        }
    }
}
