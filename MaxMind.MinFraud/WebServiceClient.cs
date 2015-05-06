using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MaxMind.MinFraud.Exception;
using MaxMind.MinFraud.Request;
using MaxMind.MinFraud.Response;
using MaxMind.MinFraud.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MaxMind.MinFraud
{
    public class WebServiceClient
    {
        private static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        private const string BasePath = "/minfraud/v2.0/";
        private readonly HttpClient _httpClient;
        private readonly List<string> _locales;
//        private readonly TimeSpan timeout;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WebServiceClient" /> class.
        /// </summary>
        /// <param name="userId">The user unique identifier.</param>
        /// <param name="licenseKey">The license key.</param>
        /// <param name="locales">List of locale codes to use in name property from most preferred to least preferred.</param>
        /// <param name="host">The base url to use when accessing the service</param>
        /// <param name="timeout">Timeout for connection to web service.</param>
        public WebServiceClient(
            int userId,
            string licenseKey,
            List<string> locales = null,
            string host = "minfraud.maxmind.com",
            TimeSpan? timeout = null,
            HttpMessageHandler httpMessageHandler = null
            )
        {
            _locales = locales ?? new List<string> {"en"};
//            this.timeout = null;
            _httpClient = new HttpClient(httpMessageHandler ?? new HttpClientHandler())
            {
                BaseAddress = new UriBuilder("https", host, -1, BasePath).Uri,
//                Timeout = timeout,
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
        }

        /// <summary>
        /// </summary>
        /// <returns>An <see cref="Task<Insights>" /></returns>
        public async Task<Insights> InsightsAsync(MinFraudRequest request)
        {
            return await MakeRequest<Insights>(request);
        }

        /// <summary>
        /// </summary>
        /// <returns>An <see cref="Task<Score></Score>" /></returns>
        public async Task<Score> ScoreAsync(MinFraudRequest request)
        {
            return await MakeRequest<Score>(request);
        }

        private async Task<T> MakeRequest<T>(MinFraudRequest request) where T : Score
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            settings.Converters.Add(new IPAddressConverter());
            settings.Converters.Add(new StringEnumConverter());
            var requestBody = JsonConvert.SerializeObject(request, settings);

            var requestPath = typeof (T).Name.ToLower();
            var response = await _httpClient.PostAsync(requestPath,
                new StringContent(requestBody, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            // XXX - this is ugly. Previously we used response.RequestMessage, but the
            // mocking package we use doesn't set RequestMessage:
            // https://github.com/richardszalay/mockhttp/issues/7
            // Once that is fixed, we should switch back.
            var uri = new Uri(_httpClient.BaseAddress.ToString() + requestPath);

            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response, uri);
            }
            return await HandleSuccess<T>(response, uri);
        }

        private static async Task<T> HandleSuccess<T>(HttpResponseMessage response, Uri uri) where T : Score
        {
            int length;
            var parsedOk = int.TryParse(response.Content.Headers.GetValues("Content-Length").FirstOrDefault(),
                out length);
            if (!parsedOk || length <= 0)
            {
                throw new HttpException(
                    $"Received a 200 response for minFraud {typeof (T).Name} but there was no message body",
                    response.StatusCode, uri);
            }
            var contentType = response.Content.Headers.GetValues("Content-Type")?.FirstOrDefault();
            if (contentType == null || !contentType.Contains("json"))
            {
                throw new MinFraudException(
                    $"Received a 200 response for  {typeof (T).Name} but it does not appear to be JSON: {contentType}");
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

        private async Task HandleError(HttpResponseMessage response, Uri uri)
        {
            var status = (int) response.StatusCode;

            if (status >= 400 && status < 500)
            {
                await Handle4xxStatus(response, uri);
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

        private async Task Handle4xxStatus(HttpResponseMessage response, Uri uri)
        {
            var status = (int) response.StatusCode;

            // The null guard is primarily because our unit testing mock library does not
            // set Content for the default response.
            var content = response.Content != null ? await response.Content.ReadAsStringAsync() : null;

            if (string.IsNullOrEmpty(content))
            {
                throw new HttpException(
                    $"Received a {status} error for {uri} with no body",
                    response.StatusCode, uri);
            }

            try
            {
                var error = JsonConvert.DeserializeObject<WebServiceError>(content);

                HandleErrorWithJsonBody(error, response, content, uri);
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
            string content, Uri uri)
        {
            if (error.Code == null || error.Error == null)
                throw new HttpException(
                    $"Error response contains JSON but it does not specify code or error keys: {content}",
                    response.StatusCode,
                    uri);
            switch (error.Code)
            {
                case "AUTHORIZATION_INVALID":
                case "LICENSE_KEY_REQUIRED":
                case "USER_ID_REQUIRED":
                    throw new AuthenticationException(error.Error);
                case "INSUFFICIENT_FUNDS":
                    throw new InsufficientFundsException(error.Error);

                default:
                    throw new InvalidRequestException(error.Error, error.Code);
            }
        }
    }
}