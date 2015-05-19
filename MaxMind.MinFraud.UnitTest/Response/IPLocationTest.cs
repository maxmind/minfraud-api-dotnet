using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IPLocationTest
    {
        [Test]
        public void TestIPLocation()
        {
            string time = "2015-04-19T12:59:23-01:00";

            var loc = new JObject
            {
                {"country", new JObject {{"is_high_risk", true}}},
                {"location", new JObject {{"local_time", time}}}
            }.ToObject<IPLocation>();

            Assert.AreEqual(time, loc.Location.LocalTime.ToString("yyyy-MM-ddTHH:mm:ssK"));
            Assert.AreEqual(true, loc.Country.IsHighRisk);
        }
    }
}