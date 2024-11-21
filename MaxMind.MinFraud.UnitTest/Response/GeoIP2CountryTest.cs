using MaxMind.GeoIP2.Model;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2CountryTest
    {
        [Fact]
        public void TestIsHighRisk()
        {
            var country = JsonSerializer.Deserialize<Country>(
                """
                {
                    "is_high_risk": true,
                    "is_in_european_union": true
                }
                """)!;

            Assert.True(country.IsInEuropeanUnion);
        }
    }
}
