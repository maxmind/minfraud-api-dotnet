using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class ScoreTest
    {
        [Fact]
        public void TestScore()
        {
            var id = "b643d445-18b2-4b9d-bad4-c9c4366e402a";
            var score = JsonSerializer.Deserialize<Score>(
                $$"""
                  {
                      "id": "{{id}}",
                      "funds_remaining": 1.20,
                      "queries_remaining": 123,
                      "disposition": {"action": "accept"},
                      "ip_address": {"risk": 0.01},
                      "risk_score": 0.01,
                      "warnings": [{"code": "INVALID_INPUT"}]
                  }
                  """)!;

            Assert.Equal(id, score.Id.ToString());
            Assert.Equal(1.20m, score.FundsRemaining);
            Assert.Equal(123, score.QueriesRemaining);
            Assert.Equal("accept", score.Disposition.Action);
            Assert.Equal(0.01, score.IPAddress.Risk);
            Assert.Equal(0.01, score.RiskScore);
            Assert.Equal("INVALID_INPUT", score.Warnings[0].Code);
        }
    }
}