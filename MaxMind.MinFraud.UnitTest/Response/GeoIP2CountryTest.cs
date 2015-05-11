using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2CountryTest
    {
        [Test]
        public void TestIsHighRisk()
        {
            var country = new JObject {{"is_high_risk", true}}.ToObject<GeoIP2Country>();
            Assert.AreEqual(true, country.IsHighRisk);
        }
    }
}