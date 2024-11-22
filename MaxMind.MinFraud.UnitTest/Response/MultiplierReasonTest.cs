using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class MultiplierReasonTest
    {
        [Fact]
        public void TestMultiplierReason()
        {
            var code = "ANONYMOUS_IP";
            var msg = "Risk due to IP being an Anonymous IP";

            var reason = JsonSerializer.Deserialize<MultiplierReason>(
                $$"""
                      {
                          "code": "{{code}}",
                          "reason": "{{msg}}"
                      }
                      """)!;

            Assert.Equal(code, reason.Code);
            Assert.Equal(msg, reason.Reason);
        }
    }
}
