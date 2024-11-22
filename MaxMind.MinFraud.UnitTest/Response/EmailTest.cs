using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailTest
    {
        [Fact]
        public void TestEmail()
        {
            var email = JsonSerializer.Deserialize<Email>(
                """
                {
                    "domain": { "first_seen": "2014-02-03" },
                    "first_seen": "2017-01-02",
                    "is_disposable": true,
                    "is_free": true,
                    "is_high_risk": true
                }
                """)!;

            Assert.Equal("2014-02-03", email.Domain.FirstSeen?.ToString("yyyy-MM-dd"));
            Assert.Equal("2017-01-02", email.FirstSeen?.ToString("yyyy-MM-dd"));
            Assert.True(email.IsDisposable);
            Assert.True(email.IsFree);
            Assert.True(email.IsHighRisk);
        }
    }
}