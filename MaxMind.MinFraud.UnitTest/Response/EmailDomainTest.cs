using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailDomainTest
    {
        [Fact]
        public void TestEmailDomain()
        {
            var domain = new JObject
            {
                {"first_seen", "2017-01-02" },
            }.ToObject<EmailDomain>()!;

            Assert.Equal("2017-01-02", domain.FirstSeen?.ToString("yyyy-MM-dd"));
        }
    }
}