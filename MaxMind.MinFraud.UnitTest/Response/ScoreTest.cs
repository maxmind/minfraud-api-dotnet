using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class ScoreTest
    {
        [Test]
        public void TestScore()
        {
            var id = "b643d445-18b2-4b9d-bad4-c9c4366e402a";
            var insights = new JObject
            {
                {"id", id},
                {"credits_remaining", 123},
                {"risk_score", 0.01},
                {"warnings", new JArray {new JObject {{"code", "INVALID_INPUT"}}}}
            }.ToObject<Insights>();

            Assert.AreEqual(id, insights.Id.ToString());
            Assert.AreEqual(123, insights.CreditsRemaining);
            Assert.AreEqual(0.01, insights.RiskScore);
            Assert.AreEqual("INVALID_INPUT", insights.Warnings[0].Code);
        }
    }
}