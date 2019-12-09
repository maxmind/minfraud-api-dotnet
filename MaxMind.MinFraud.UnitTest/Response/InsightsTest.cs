using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class InsightsTest
    {
        [Fact]
        public void TestInsights()
        {
            var id = "b643d445-18b2-4b9d-bad4-c9c4366e402a";
            var insights = new JObject
            {
                {"id", id},
                {"ip_address", new JObject {{"country", new JObject {{"iso_code", "US"}}}}},
                {"credit_card", new JObject {{"is_prepaid", true}}},
                {"device", new JObject {{"id", id}}},
                {"disposition", new JObject {{"action", "accept"}}},
                {"email", new JObject {{"is_free", true}}},
                {"shipping_address", new JObject {{"is_in_ip_country", true}}},
                {"billing_address", new JObject {{"is_in_ip_country", true}}},
                {
                    "funds_remaining",
                    1.20
                },
                {
                    "queries_remaining",
                    123
                },
                {
                    "risk_score",
                    0.01
                },
                {"warnings", new JArray {new JObject {{"code", "INVALID_INPUT"}}}}
            }.ToObject<Insights>()!;

            Assert.Equal("US", insights.IPAddress.Country.IsoCode);
            Assert.True(insights.CreditCard.IsPrepaid);
            Assert.Equal(id, insights.Device.Id.ToString());
            Assert.Equal("accept", insights.Disposition.Action);
            Assert.True(insights.Email.IsFree);
            Assert.True(insights.ShippingAddress.IsInIPCountry);
            Assert.True(insights.BillingAddress.IsInIPCountry);
            Assert.Equal(id, insights.Id.ToString());
            Assert.Equal(1.20m, insights.FundsRemaining);
            Assert.Equal(123, insights.QueriesRemaining);
            Assert.Equal(0.01, insights.RiskScore);
            Assert.Equal("INVALID_INPUT", insights.Warnings[0].Code);
        }
    }
}