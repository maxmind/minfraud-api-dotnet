using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using static MaxMind.MinFraud.UnitTest.Request.TestHelper;

namespace MaxMind.MinFraud.UnitTest
{
    public class WebServiceClientTest
    {
        [Fact]
        public async Task TestFullScoreRequest()
        {
            var responseContent = ReadJsonFile("score-response");
            var client = CreateSuccessClient("score", responseContent);
            var request = CreateFullRequest();
            var response = await client.ScoreAsync(request);
            CompareJson(responseContent, response, false);
        }

        [Fact]
        public async Task TestFullInsightsRequest()
        {
            var responseContent = ReadJsonFile("insights-response");
            var client = CreateSuccessClient("insights", responseContent);
            var request = CreateFullRequest();
            var response = await client.InsightsAsync(request);
            CompareJson(responseContent, response, true);

            // The purpose here is to test that SetLocales worked as expected
            Assert.Equal("London", response.IPAddress.City.Name);
            Assert.Equal("United Kingdom", response.IPAddress.Country.Name);

            Assert.True(response.IPAddress.Country.IsInEuropeanUnion);
            Assert.False(response.IPAddress.RegisteredCountry.IsInEuropeanUnion);
            Assert.False(response.IPAddress.RepresentedCountry.IsInEuropeanUnion);
        }

        [Fact]
        public async Task TestFullFactorsRequest()
        {
            var responseContent = ReadJsonFile("factors-response");
            var client = CreateSuccessClient("factors", responseContent);
            var request = CreateFullRequest();
            var response = await client.FactorsAsync(request);
            CompareJson(responseContent, response, true);
        }

        private void CompareJson(string responseContent, object response, bool mungeIPAddress)
        {
            var expectedResponse = JsonConvert.DeserializeObject<JObject>(responseContent);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            var actualResponse =
                JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(response, settings));

            if (mungeIPAddress)
            {
                // These are empty objects. There isn't an easy way to ignore them with JSON.NET.
                var ipAddress = (JObject) expectedResponse["ip_address"];
                ipAddress.Add("maxmind", new JObject());
                ipAddress.Add("postal", new JObject());
                var representedCountry = new JObject {{"names", new JObject()}};
                ipAddress.Add("represented_country", representedCountry);
            }

            var areEqual = JToken.DeepEquals(expectedResponse, actualResponse);
            if (!areEqual)
            {
                Console.WriteLine($"Expected: {expectedResponse}");
                Console.WriteLine($"Actual: {actualResponse}");
            }

            Assert.True(areEqual);
        }

        [Fact]
        public async void Test200WithNoBody()
        {
            var client = CreateSuccessClient("insights", "");
            var request = CreateFullRequest();

            var exception = await Record.ExceptionAsync(async () => await client.InsightsAsync(request));
            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Contains("Received a 200 response for minFraud Insights but there was no message body",
                exception.Message);
        }

        [Fact]
        public async void Test200WithNoContentLength()
        {
            var expectedResponse = ReadJsonFile("score-response");
            var content = new StringContent(expectedResponse);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, "https://minfraud.maxmind.com/minfraud/v2.0/score")
                .WithHeaders("Accept", "application/json")
                .With(request => VerifyRequestFor("score", request))
                .Respond(message);

            var client = new WebServiceClient(6, "0123456789", httpMessageHandler: mockHttp);
            var response = await client.ScoreAsync(CreateFullRequest());
            CompareJson(expectedResponse, response, false);
        }

        [Fact]
        public async void Test200WithInvalidJson()
        {
            var client = CreateSuccessClient("insights", "{");
            var request = CreateFullRequest();

            var exception = await Record.ExceptionAsync(async () => await client.InsightsAsync(request));
            Assert.NotNull(exception);
            Assert.IsType<MinFraudException>(exception);
            Assert.Contains("Received a 200 response but not decode it as JSON", exception.Message);
        }

        [Fact]
        public async void TestInsufficientFunds()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.PaymentRequired,
                "application/json",
                "{\"code\":\"INSUFFICIENT_FUNDS\",\"error\":\"out of funds\"}"
            ));

            Assert.NotNull(exception);
            Assert.IsType<InsufficientFundsException>(exception);
            Assert.Contains("out of funds", exception.Message);
        }

        [Theory]
        [InlineData("AUTHORIZATION_INVALID")]
        [InlineData("LICENSE_KEY_REQUIRED")]
        [InlineData("USER_ID_REQUIRED")]
        public async void TestInvalidAuth(string code)
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.Unauthorized,
                "application/json",
                $"{{\"code\":\"{code}\",\"error\":\"Invalid auth\"}}"
            ));
            Assert.NotNull(exception);
            Assert.IsType<AuthenticationException>(exception);
            Assert.Contains("Invalid auth", exception.Message);
        }

        [Fact]
        public async void TestPermissionRequired()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.PaymentRequired,
                "application/json",
                "{\"code\":\"PERMISSION_REQUIRED\",\"error\":\"Permission required\"}"
            ));
            Assert.NotNull(exception);
            Assert.IsType<PermissionRequiredException>(exception);
            Assert.Contains("Permission required", exception.Message);
        }

        [Fact]
        public async void TestInvalidRequest()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{\"code\":\"IP_ADDRESS_INVALID\",\"error\":\"IP invalid\"}"
            ));

            Assert.NotNull(exception);
            Assert.IsType<InvalidRequestException>(exception);
            Assert.Contains("IP invalid", exception.Message);
        }

        [Fact]
        public async void Test400WithInvalidJson()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{blah}"
            ));

            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but it did not include the expected JSON body: {blah}",
                exception.Message);
        }

        [Fact]
        public async void Test400WithNoBody()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                ""
            ));
            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal("Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights with no body",
                exception.Message);
        }

        [Fact]
        public async void Test400WithUnexpectedContentType()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "text/plain",
                "text"
            ));
            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but it did not include the expected JSON body: text",
                exception.Message);
        }

        [Fact]
        public async void Test400WithUnexpectedJson()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{\"not\":\"expected\"}"
            ));

            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Error response contains JSON but it does not specify code or error keys: {\"not\":\"expected\"}",
                exception.Message);
        }

        [Fact]
        public async void Test300()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.MultipleChoices,
                "application/json",
                ""));

            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Received an unexpected HTTP status (300) for https://minfraud.maxmind.com/minfraud/v2.0/insights",
                exception.Message);
        }

        [Fact]
        public async void Test500()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.InternalServerError,
                "application/json",
                ""
            ));

            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal("Received a server (500) error for https://minfraud.maxmind.com/minfraud/v2.0/insights",
                exception.Message);
        }

        private WebServiceClient CreateSuccessClient(string service, string responseContent)
        {
            return CreateClient(
                service,
                HttpStatusCode.OK,
                $"application/vnd.maxmind.com-minfraud-{service}+json",
                responseContent
            );
        }

        private async Task CreateInsightsError(HttpStatusCode status, string contentType, string responseContent)
        {
            var client = CreateClient(
                "insights",
                status,
                contentType,
                responseContent
            );
            await client.InsightsAsync(CreateFullRequest());
        }

        private WebServiceClient CreateClient(string service, HttpStatusCode status, string contentType,
            string responseContent)
        {
            StringContent content = new StringContent(responseContent);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Headers.Add("Content-Length", responseContent.Length.ToString());
            HttpResponseMessage message = new HttpResponseMessage(status)
            {
                Content = content
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, $"https://minfraud.maxmind.com/minfraud/v2.0/{service}")
                .WithHeaders("Accept", "application/json")
                .With(request => VerifyRequestFor(service, request))
                .Respond(message);

            return new WebServiceClient(6, "0123456789", httpMessageHandler: mockHttp);
        }
    }
}