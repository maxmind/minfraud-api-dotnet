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
        private static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        private const string BasePath = "/minfraud/v2.0/";
        private readonly HttpClient _httpClient;
        private readonly List<string> _locales;
        private bool _disposed;

        /// <summary>
        /// Constructor for minFraud web service client.
        /// </summary>
        /// <param name="userId">Your MaxMind user ID.</param>
        /// <param name="licenseKey">Your MaxMind license key.</param>
        /// <param name="locales">A list of locale codes to use for name property.</param>
        /// <param name="host">The host to use when connecting to the web service.</param>
        /// <param name="timeout">The timeout to use for the request.</param>
        /// <param name="httpMessageHandler">Handler to use in request. For unit testing only.</param>
        public WebServiceClient(
            int userId,
            string licenseKey,
            IEnumerable<string> locales = null,
            string host = "minfraud.maxmind.com",
            TimeSpan? timeout = null,
            HttpMessageHandler httpMessageHandler = null
            )
        {
            _locales = locales == null ? new List<string> { "en" } : new List<string>(locales);
            _httpClient = new HttpClient(httpMessageHandler ?? new HttpClientHandler())
            {
                BaseAddress = new UriBuilder("https", host, -1, BasePath).Uri,
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            Encoding.ASCII.GetBytes(
                                $"{userId}:{licenseKey}"))),
                    Accept = {new MediaTypeWithQualityHeaderValue("application/json")},
                    UserAgent = {new ProductInfoHeaderValue("minFraud-api-dotnet", Version.ToString())}
                }
            };
            if (timeout != null)
            {
                _httpClient.Timeout = timeout.Value;
            }
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
            var insights = await MakeRequest<Insights>(transaction).ConfigureAwait(false);
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
            return await MakeRequest<Score>(transaction).ConfigureAwait(false);
        }

        private async Task<T> MakeRequest<T>(Transaction request) where T : Score
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new StringEnumConverter());
            var requestBody = JsonConvert.SerializeObject(request, settings);

            var requestPath = typeof(T).Name.ToLower();
            var response = await _httpClient.PostAsync(requestPath,
                new StringContent(requestBody, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response).ConfigureAwait(false);
            }
            return await HandleSuccess<T>(response).ConfigureAwait(false);
        }

        private static async Task<T> HandleSuccess<T>(HttpResponseMessage response) where T : Score
        {
            int length;
            var parsedOk = int.TryParse(response.Content.Headers.GetValues("Content-Length").FirstOrDefault(),
                out length);
            if (!parsedOk || length <= 0)
            {
                throw new HttpException(
                    $"Received a 200 response for minFraud {typeof(T).Name} but there was no message body",
                    response.StatusCode, response.RequestMessage.RequestUri);
            }
            var contentType = response.Content.Headers.GetValues("Content-Type")?.FirstOrDefault();
            if (contentType == null || !contentType.Contains("json"))
            {
                throw new MinFraudException(
                    $"Received a 200 response for  {typeof(T).Name} but it does not appear to be JSON: {contentType}");
            }
            using (var s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                try
                {
                    return serializer.Deserialize<T>(reader);
                }
                catch (JsonSerializationException ex)
                {
                    throw new MinFraudException(
                        "Received a 200 response but not decode it as JSON", ex);
                }
            }
        }

        private async Task HandleError(HttpResponseMessage response)
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

        private async Task Handle4xxStatus(HttpResponseMessage response)
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
                var error = JsonConvert.DeserializeObject<WebServiceError>(content);

                HandleErrorWithJsonBody(error, response, content);
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
                case "AUTHORIZATION_INVALID":
                case "LICENSE_KEY_REQUIRED":
                case "USER_ID_REQUIRED":
                    throw new AuthenticationException(error.Error);
                case "INSUFFICIENT_FUNDS":
                    throw new InsufficientFundsException(error.Error);

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
            }

            _disposed = true;
        }
    }
}