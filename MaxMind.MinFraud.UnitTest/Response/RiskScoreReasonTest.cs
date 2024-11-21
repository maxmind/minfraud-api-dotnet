using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class RiskScoreReasonTest
    {
        [Fact]
        public void TestRiskScoreReason()
        {
            var reason = JsonSerializer.Deserialize<RiskScoreReason>(
                $$"""
                      {
                          "multiplier": 45.0,
                          "reasons": [
                              {
                                  "code": "ANONYMOUS_IP",
                                  "reason": "Risk due to IP being an Anonymous IP"
                              }
                          ]
                      }
                    """)!;

            Assert.Equal(45.0, reason.Multiplier);
            Assert.Equal("ANONYMOUS_IP", reason.Reasons[0].Code);
            Assert.Equal(
                "Risk due to IP being an Anonymous IP",
                reason.Reasons[0].Reason
            );
        }
    }
}
