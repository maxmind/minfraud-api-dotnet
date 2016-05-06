using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class SubScoresTest
    {
        [Test]
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

            Assert.AreEqual(0.01, subscores.AvsResult);
            Assert.AreEqual(0.02, subscores.BillingAddress);
            Assert.AreEqual(0.03, subscores.BillingAddressDistanceToIPLocation);
            Assert.AreEqual(0.04, subscores.Browser);
            Assert.AreEqual(0.05, subscores.Chargeback);
            Assert.AreEqual(0.06, subscores.Country);
            Assert.AreEqual(0.07, subscores.CountryMismatch);
            Assert.AreEqual(0.08, subscores.CvvResult);
            Assert.AreEqual(0.09, subscores.EmailAddress);
            Assert.AreEqual(0.10, subscores.EmailDomain);
            Assert.AreEqual(0.11, subscores.EmailTenure);
            Assert.AreEqual(0.12, subscores.IPTenure);
            Assert.AreEqual(0.13, subscores.IssuerIdNumber);
            Assert.AreEqual(0.14, subscores.OrderAmount);
            Assert.AreEqual(0.15, subscores.PhoneNumber);
            Assert.AreEqual(0.16, subscores.ShippingAddressDistanceToIPLocation);
            Assert.AreEqual(0.17, subscores.TimeOfDay);
        }
    }
}