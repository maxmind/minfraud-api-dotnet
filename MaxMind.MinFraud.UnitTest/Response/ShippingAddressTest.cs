using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;
using static MaxMind.MinFraud.UnitTest.Response.AddressTestHelper;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class ShippingAddressTest
    {
        [Fact]
        public void TestShippingAddress()
        {
            var address = JsonSerializer.Deserialize<ShippingAddress>(
                @"
                    {
                        ""is_in_ip_country"": true,
                        ""latitude"": 43.1,
                        ""longitude"": 32.1,
                        ""distance_to_ip_location"": 100,
                        ""is_postal_in_city"": true,
                        ""is_high_risk"": false,
                        ""distance_to_billing_address"": 200
                    }
                ")!;

            TestAddress(address);

            Assert.False(address.IsHighRisk);
            Assert.Equal(200, address.DistanceToBillingAddress);
        }
    }
}
