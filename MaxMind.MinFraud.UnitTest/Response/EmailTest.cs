using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailTest
    {
        [Fact]
        public void TestEmail()
        {
            var email = new JObject
            {
                {"first_seen", "2017-01-02" },
                {"is_disposable", true},
                {"is_free", true},
                {"is_high_risk", true}
            }.ToObject<Email>()!;

            Assert.Equal("2017-01-02", email.FirstSeen?.ToString("yyyy-MM-dd"));
            Assert.True(email.IsDisposable);
            Assert.True(email.IsFree);
            Assert.True(email.IsHighRisk);
        }
    }
}