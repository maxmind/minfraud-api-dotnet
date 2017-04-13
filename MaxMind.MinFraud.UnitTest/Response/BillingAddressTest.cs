using MaxMind.MinFraud.Response;
using static MaxMind.MinFraud.UnitTest.Response.AddressTestHelper;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class BillingAddressTest
    {
        [Fact]
        public void TestBillingAddress()
        {
            var address = new JObject
            {
                {"is_in_ip_country", true},
                {"latitude", 43.1},
                {"longitude", 32.1},
                {"distance_to_ip_location", 100},
                {"is_postal_in_city", true}
            }.ToObject<BillingAddress>();

            TestAddress(address);
        }
    }
}