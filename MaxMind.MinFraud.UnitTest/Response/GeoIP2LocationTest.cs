using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2LocationTest
    {
        [Fact]
        public void TestGetLocalTime()
        {
            var time = "2015-04-19T12:59:23-01:00";
            var location = JsonSerializer.Deserialize<GeoIP2Location>(
                $$"""{"local_time": "{{time}}" }""")!;

            Assert.Equal(time, location.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}