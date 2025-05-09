using System.Text.Json;
using Xunit;
using Phone = MaxMind.MinFraud.Response.Phone;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class PhoneTest
    {
        [Fact]
        public void TestPhone()
        {
            var country = "US";
            var networkOperator = "Verizon";
            var numberType = "mobile";
            var phone = JsonSerializer.Deserialize<Phone>(
                $$"""
                      {
                          "country": "{{country}}",
                          "is_voip": true,
                          "matches_postal": false,
                          "network_operator": "{{networkOperator}}",
                          "number_type": "{{numberType}}"
                      }
                      """)!;

            Assert.Equal(country, phone.Country);
            Assert.True(phone.IsVoip);
            Assert.False(phone.MatchesPostal);
            Assert.Equal(networkOperator, phone.NetworkOperator);
            Assert.Equal(numberType, phone.NumberType);
        }
    }
}
