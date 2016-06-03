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
            var score = new JObject
            {
                {"id", id},
                {"funds_remaining", 1.20},
                {"queries_remaining", 123},
                {"ip_address", new JObject { { "risk", 0.01} } },
                {"risk_score", 0.01},
                {"warnings", new JArray {new JObject {{"code", "INVALID_INPUT"}}}}
            }.ToObject<Score>();

            Assert.AreEqual(id, score.Id.ToString());
            Assert.AreEqual(1.20, score.FundsRemaining);
            Assert.AreEqual(123, score.QueriesRemaining);
            Assert.AreEqual(0.01, score.IPAddress.Risk);
            Assert.AreEqual(0.01, score.RiskScore);
            Assert.AreEqual("INVALID_INPUT", score.Warnings[0].Code);
        }
    }
}
