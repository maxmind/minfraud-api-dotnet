using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using System.Threading.Tasks;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static MaxMind.MinFraud.UnitTest.Request.TestHelper;
using RichardSzalay.MockHttp;

namespace MaxMind.MinFraud.UnitTest
{
    [TestFixture]
    public class WebServiceClientTest
    {
        [Test]
        public void TestMethod1()
        {
        }


        [Test]
        public async Task TestFullScoreRequest()
        {
            var responseContent = ReadJsonFile("score-response");
            var client = CreateSuccessClient("score", responseContent);
            var request = CreateFullRequest();
            var response = await client.ScoreAsync(request);
            CompareJson(responseContent, response);
        }


        [Test]
        public async Task TestFullInsightsRequest()
        {
            var responseContent = ReadJsonFile("insights-response");
            var client = CreateSuccessClient("insights", responseContent);
            var request = CreateFullRequest();
            var response = await client.InsightsAsync(request);
            CompareJson(responseContent, response);
        }


        private void CompareJson(string responseContent, Object response)
        {
            var expectedResponse = JsonConvert.DeserializeObject<JObject>(responseContent);
            var actualResponse = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(response));

            var areEqual = JToken.DeepEquals(expectedResponse, actualResponse);
            if (!areEqual)
            {
                Console.WriteLine($"Expected: {expectedResponse}");
                Console.WriteLine($"Actual: {actualResponse}");
            }

            Assert.That(areEqual);
        }


        [Test]
        public void Test200WithItest200WithNoBodynvalidJson()
        {
            var client = CreateSuccessClient("insights", "");
            var request = CreateFullRequest();

            Assert.That(async () => await client.InsightsAsync(request),
                Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo("Received a 200 response for minFraud Insights but there was no message body"));
        }

        [Test]
        public void Test200WithInvalidJson()
        {
            var client = CreateSuccessClient("insights", "{");
            var request = CreateFullRequest();
            Assert.That(async () => await client.InsightsAsync(request),
                Throws.TypeOf<MinFraudException>()
                    .And.Message.EqualTo("Received a 200 response but not decode it as JSON"));
        }

        [Test]
        public void TestInsufficientFunds()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.PaymentRequired,
                "application/json",
                "{\"code\":\"INSUFFICIENT_FUNDS\",\"error\":\"out of funds\"}"
                ),
                Throws.TypeOf<InsufficientFundsException>()
                    .And.Message.EqualTo("out of funds"));
        }

        [Test]
        [TestCase("AUTHORIZATION_INVALID")]
        [TestCase("LICENSE_KEY_REQUIRED")]
        [TestCase("USER_ID_REQUIRED")]
        public void TestInvalidAuth(string code)
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.Unauthorized,
                "application/json",
                $"{{\"code\":\"{code}\",\"error\":\"Invalid auth\"}}"
                ),
                Throws.TypeOf<AuthenticationException>()
                    .And.Message.EqualTo("Invalid auth"));
        }

        [Test]
        public void TestInvalidRequest()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{\"code\":\"IP_ADDRESS_INVALID\",\"error\":\"IP invalid\"}"
                ),
                Throws.TypeOf<InvalidRequestException>()
                    .And.Message.EqualTo("IP invalid"));
        }

        [Test]
        public void Test400WithInvalidJson()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{blah}"
                ),
                Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but it did not include the expected JSON body: {blah}"));
        }

        [Test]
        public void Test400WithNoBody()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                ""
                ),
                Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights with no body"));
        }

        [Test]
        public void Test400WithUnexpectedContentType()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "text/plain",
                "text"
                ),
                Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Received a 400 error for https://minfraud.maxmind.com/minfraud/v2.0/insights but it did not include the expected JSON body: text"));
        }

        [Test]
        public void Test400WithUnexpectedJson()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.BadRequest,
                "application/json",
                "{\"not\":\"expected\"}"
                ),
                Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Error response contains JSON but it does not specify code or error keys: {\"not\":\"expected\"}"));
        }

        [Test]
        public void Test300()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.MultipleChoices,
                "application/json",
                "")
                , Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Received an unexpected HTTP status (300) for https://minfraud.maxmind.com/minfraud/v2.0/insights"));
        }

        [Test]
        public void Test500()
        {
            Assert.That(async () => await CreateInsightsError(
                HttpStatusCode.InternalServerError,
                "application/json",
                ""
                ), Throws.TypeOf<HttpException>()
                    .And.Message.EqualTo(
                        "Received a server (500) error for https://minfraud.maxmind.com/minfraud/v2.0/insights"));
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