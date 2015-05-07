using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2LocationTest
    {
        [Test]
        public void TestGetLocalTime()
        {
            var time = "2015-04-19T12:59:23-01:00";
            var location = new JObject {{"local_time", time}}.ToObject<GeoIP2Location>();
            Assert.AreEqual(time, location.LocalTime.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}