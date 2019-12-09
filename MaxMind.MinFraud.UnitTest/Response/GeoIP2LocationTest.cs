using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2LocationTest
    {
        [Fact]
        public void TestGetLocalTime()
        {
            var time = "2015-04-19T12:59:23-01:00";
            var location = new JObject { { "local_time", time } }.ToObject<GeoIP2Location>()!;
            Assert.Equal(time, location.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}