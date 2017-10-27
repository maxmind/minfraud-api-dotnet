using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IPAddressTest
    {
        [Fact]
        public void TestIPAddress()
        {
            string time = "2015-04-19T12:59:23-01:00";

            var address = new JObject
            {
                {"risk", 99},
                {"country", new JObject {{"is_high_risk", true}}},
                {"location", new JObject {{"local_time", time}}}
            }.ToObject<IPAddress>();

            Assert.Equal(99, address.Risk);
            Assert.Equal(time, address.Location.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
            Assert.True(address.Country.IsHighRisk);
        }
    }
}
