using MaxMind.MinFraud.Exception;
using MaxMind.MinFraud.Request;
using MaxMind.MinFraud.Util;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static MaxMind.MinFraud.UnitTest.Request.TestHelper;

namespace MaxMind.MinFraud.UnitTest
{
    public class WebServiceClientTest
    {
        private readonly ITestOutputHelper output;

        public WebServiceClientTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task TestFullScoreRequest()
        {
            var responseContent = ReadJsonFile("score-response");
            var client = CreateSuccessClient("score", responseContent);
            var request = CreateFullRequest();
            var response = await client.ScoreAsync(request);
            CompareJson(responseContent, response);
        }

        [Fact]
        public async Task TestFullInsightsRequest()
        {
            var responseContent = ReadJsonFile("insights-response");
            var client = CreateSuccessClient("insights", responseContent);
            var request = CreateFullRequest();
            var response = await client.InsightsAsync(request);
            CompareJson(responseContent, response);

            // The purpose here is to test that SetLocales worked as expected
            Assert.Equal("London", response.IPAddress.City.Name);
            Assert.Equal("United Kingdom", response.IPAddress.Country.Name);

            Assert.True(response.IPAddress.Country.IsInEuropeanUnion);
            Assert.True(response.IPAddress.RegisteredCountry.IsInEuropeanUnion);
            Assert.True(response.IPAddress.RepresentedCountry.IsInEuropeanUnion);
            Assert.True(response.IPAddress.Traits.IsResidentialProxy);

            Assert.Equal("310", response.IPAddress.Traits.MobileCountryCode);
            Assert.Equal("004", response.IPAddress.Traits.MobileNetworkCode);
        }

        [Fact]
        public async Task TestFullFactorsRequest()
        {
            var responseContent = ReadJsonFile("factors-response");
            var client = CreateSuccessClient("factors", responseContent);
            var request = CreateFullRequest();
            var response = await client.FactorsAsync(request);
            CompareJson(responseContent, response);
        }

        [Fact]
        public async Task TestFullFactorsRequestUsingConstructors()
        {
            var responseContent = ReadJsonFile("factors-response");
            var client = CreateSuccessClient("factors", responseContent);
            var request = CreateFullRequestUsingConstructors();
            var response = await client.FactorsAsync(request);
            CompareJson(responseContent, response);

            Assert.Equal("London", response.IPAddress.City.Name);
        }

        [Fact]
        public async Task TestFullReportRequest()
        {
            var client = CreateClient(
                "transactions/report",
                HttpStatusCode.NoContent,
                "application/json",
                ""
            );
            var request = new TransactionReport(
                ipAddress: IPAddress.Parse("1.1.1.1"),
                tag: TransactionReportTag.SuspectedFraud,
                chargebackCode: "AA",
                maxmindId: "a1b2c3d4",
                minfraudId: new Guid("9194a1ac-0a81-475a-bf81-9bf8543a3f8f"),
                notes: "note",
                transactionId: "txn1");
            var exception = await Record.ExceptionAsync(() => client.ReportAsync(request));

            Assert.Null(exception);
        }

        [Fact]
        public async Task TestWebServiceClientOptionsConstructor()
        {
            var responseContent = ReadJsonFile("score-response");
            var content = new StringContent(responseContent);
            content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/vnd.maxmind.com-minfraud-score+json");
            content.Headers.Add("Content-Length", responseContent.Length.ToString());
            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, "https://test.maxmind.com/minfraud/v2.0/score")
                .WithHeaders("Accept", "application/json")
                .With(request => VerifyRequestFor("score", request, output))
                .Respond(_ => message);

            var options = Options.Create(new WebServiceClientOptions
            {
                AccountId = 6,
                LicenseKey = "0123456789",
                Host = "test.maxmind.com",
                Timeout = TimeSpan.FromSeconds(5),
                Locales = new List<string> { "en" }
            });

            var client = new WebServiceClient(new HttpClient(mockHttp), options);
            var request = CreateFullRequest();
            var response = await client.ScoreAsync(request);

            CompareJson(responseContent, response);
        }

        private void CompareJson(string responseContent, object response)
        {
            var expectedResponse = JsonDocument.Parse(responseContent);

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            options.Converters.Add(new NetworkConverter());

            var actualJson = JsonSerializer.Serialize(response, options);
            var actualResponse = JsonDocument.Parse(actualJson);

            var areEqual = new JsonElementComparer().JsonEquals(expectedResponse, actualResponse);
            if (!areEqual)
            {
                output.WriteLine($"Expected: {responseContent}");
                output.WriteLine($"Actual: {actualJson}");
            }

            Assert.True(areEqual);
        }

        [Fact]
        public async Task Test200WithNoBody()
        {
            var client = CreateSuccessClient("insights", "");
            var request = CreateFullRequest();

            var exception = await Record.ExceptionAsync(async () => await client.InsightsAsync(request));
            Assert.NotNull(exception);
            Assert.IsType<MinFraudException>(exception);
            Assert.Contains("Received a 200 response but could not decode it as JSON",
                exception.Message);
        }

        [Fact]
        public async Task Test200WithNoContentLength()
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
                .With(request => VerifyRequestFor("score", request, output))
                .Respond(_ => message);

            var client = new WebServiceClient(6, "0123456789", httpMessageHandler: mockHttp);
            var response = await client.ScoreAsync(CreateFullRequest());
            CompareJson(expectedResponse, response);
        }

        [Fact]
        public async Task Test200WithInvalidJson()
        {
            var client = CreateSuccessClient("insights", "{");
            var request = CreateFullRequest();

            var exception = await Record.ExceptionAsync(async () => await client.InsightsAsync(request));
            Assert.NotNull(exception);
            Assert.IsType<MinFraudException>(exception);
            Assert.Contains("Received a 200 response but could not decode it as JSON", exception.Message);
        }

        [Fact]
        public async Task TestInsufficientFunds()
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
        [InlineData("ACCOUNT_ID_REQUIRED")]
        [InlineData("AUTHORIZATION_INVALID")]
        [InlineData("LICENSE_KEY_REQUIRED")]
        public async Task TestInvalidAuth(string code)
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
        public async Task TestPermissionRequired()
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
        public async Task TestInvalidRequest()
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
        public async Task Test400WithInvalidJson()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{blah}"
            ));

            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but there was an error parsing it as JSON: {blah}",
                exception.Message);
        }

        [Fact]
        public async Task Test400WithNoBody()
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
        public async Task Test400WithUnexpectedContentType()
        {
            var exception = await Record.ExceptionAsync(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "text/plain",
                "text"
            ));
            Assert.NotNull(exception);
            Assert.IsType<HttpException>(exception);
            Assert.Equal(
                "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but there was an error parsing it as JSON: text",
                exception.Message);
        }

        [Fact]
        public async Task Test400WithUnexpectedJson()
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
        public async Task Test300()
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
        public async Task Test500()
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
            var content = new StringContent(responseContent);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Headers.Add("Content-Length", responseContent.Length.ToString());
            var message = new HttpResponseMessage(status)
            {
                Content = content
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(HttpMethod.Post, $"https://minfraud.maxmind.com/minfraud/v2.0/{service}")
                .WithHeaders("Accept", "application/json")
                .With(request => VerifyRequestFor(service, request, output))
                .Respond(_ => message);

            return new WebServiceClient(6, "0123456789", httpMessageHandler: mockHttp);
        }
    }
}
