using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class InsightsTest
    {
        [Test]
        public void TestInsights()
        {
            var id = "b643d445-18b2-4b9d-bad4-c9c4366e402a";
            var insights = new JObject
            {
                {"id", id},
                {"ip_address", new JObject {{"country", new JObject {{"iso_code", "US"}}}}},
                {"credit_card", new JObject {{"is_prepaid", true}}},
                {"device", new JObject { { "id", id } } },
                {"email", new JObject { {"is_free", true} } },
                {"shipping_address", new JObject {{"is_in_ip_country", true}}},
                {"billing_address", new JObject {{"is_in_ip_country", true}}},
                {
                    "credits_remaining",
                    123
                },
                {
                    "risk_score",
                    0.01
                },
                {"warnings", new JArray {new JObject {{"code", "INVALID_INPUT"}}}}
            }.ToObject<Insights>();

            Assert.AreEqual("US", insights.IPAddress.Country.IsoCode);
            Assert.IsTrue(insights.CreditCard.IsPrepaid);
            Assert.AreEqual(id, insights.Device.Id.ToString());
            Assert.IsTrue(insights.Email.IsFree);
            Assert.IsTrue(insights.ShippingAddress.IsInIPCountry);
            Assert.IsTrue(insights.BillingAddress.IsInIPCountry);
            Assert.AreEqual(id, insights.Id.ToString());
            Assert.AreEqual(123, insights.CreditsRemaining);
            Assert.AreEqual(0.01, insights.RiskScore);
            Assert.AreEqual("INVALID_INPUT", insights.Warnings[0].Code);
        }
    }
}