using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2CountryTest
    {
        [Fact]
        public void TestIsHighRisk()
        {
            var country = new JObject {{"is_high_risk", true}}.ToObject<GeoIP2Country>();
            Assert.Equal(true, country.IsHighRisk);
        }
    }
}