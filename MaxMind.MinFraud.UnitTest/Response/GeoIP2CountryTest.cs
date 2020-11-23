using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class GeoIP2CountryTest
    {
        [Fact]
        public void TestIsHighRisk()
        {
            var country = JsonSerializer.Deserialize<GeoIP2Country>(
                @"
                    {
                        ""is_high_risk"": true,
                        ""is_in_european_union"": true
                    }
                ")!;

            Assert.True(country.IsInEuropeanUnion);
#pragma warning disable CS0618 // Type or member is obsolete
            Assert.True(country.IsHighRisk);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
