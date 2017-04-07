using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class SubScoresTest
    {
        [Fact]
        public void TestSubscores()
        {
            var subscores = new JObject
            {
                {"avs_result", 0.01},
                {"billing_address", 0.02},
                {"billing_address_distance_to_ip_location", 0.03},
                {"browser", 0.04},
                {"chargeback", 0.05},
                {"country", 0.06},
                {"country_mismatch", 0.07},
                {"cvv_result", 0.08},
                {"email_address", 0.09},
                {"email_domain", 0.10},
                {"email_tenure", 0.11},
                {"ip_tenure", 0.12},
                {"issuer_id_number", 0.13},
                {"order_amount", 0.14},
                {"phone_number", 0.15},
                {"shipping_address_distance_to_ip_location", 0.16},
                {"time_of_day", 0.17}
            }.ToObject<Subscores>();

            Assert.Equal(0.01, subscores.AvsResult);
            Assert.Equal(0.02, subscores.BillingAddress);
            Assert.Equal(0.03, subscores.BillingAddressDistanceToIPLocation);
            Assert.Equal(0.04, subscores.Browser);
            Assert.Equal(0.05, subscores.Chargeback);
            Assert.Equal(0.06, subscores.Country);
            Assert.Equal(0.07, subscores.CountryMismatch);
            Assert.Equal(0.08, subscores.CvvResult);
            Assert.Equal(0.09, subscores.EmailAddress);
            Assert.Equal(0.10, subscores.EmailDomain);
            Assert.Equal(0.11, subscores.EmailTenure);
            Assert.Equal(0.12, subscores.IPTenure);
            Assert.Equal(0.13, subscores.IssuerIdNumber);
            Assert.Equal(0.14, subscores.OrderAmount);
            Assert.Equal(0.15, subscores.PhoneNumber);
            Assert.Equal(0.16, subscores.ShippingAddressDistanceToIPLocation);
            Assert.Equal(0.17, subscores.TimeOfDay);
        }
    }
}