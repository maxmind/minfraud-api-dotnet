using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
#pragma warning disable 0618
    public class SubScoresTest
    {
        [Fact]
        public void TestSubscores()
        {
            var subscores = JsonSerializer.Deserialize<Subscores>(
                """
                    {
                        "avs_result": 0.01,
                        "billing_address": 0.02,
                        "billing_address_distance_to_ip_location": 0.03,
                        "browser": 0.04,
                        "chargeback": 0.05,
                        "country": 0.06,
                        "country_mismatch": 0.07,
                        "cvv_result": 0.08,
                        "device": 0.09,
                        "email_address": 0.10,
                        "email_domain": 0.11,
                        "email_local_part": 0.12,
                        "email_tenure": 0.13,
                        "ip_tenure": 0.14,
                        "issuer_id_number": 0.15,
                        "order_amount": 0.16,
                        "phone_number": 0.17,
                        "shipping_address": 0.18,
                        "shipping_address_distance_to_ip_location": 0.19,
                        "time_of_day": 0.20
                    }
                    """)!;

            Assert.Equal(0.01, subscores.AvsResult);
            Assert.Equal(0.02, subscores.BillingAddress);
            Assert.Equal(0.03, subscores.BillingAddressDistanceToIPLocation);
            Assert.Equal(0.04, subscores.Browser);
            Assert.Equal(0.05, subscores.Chargeback);
            Assert.Equal(0.06, subscores.Country);
            Assert.Equal(0.07, subscores.CountryMismatch);
            Assert.Equal(0.08, subscores.CvvResult);
            Assert.Equal(0.09, subscores.Device);
            Assert.Equal(0.10, subscores.EmailAddress);
            Assert.Equal(0.11, subscores.EmailDomain);
            Assert.Equal(0.12, subscores.EmailLocalPart);
            Assert.Equal(0.15, subscores.IssuerIdNumber);
            Assert.Equal(0.16, subscores.OrderAmount);
            Assert.Equal(0.17, subscores.PhoneNumber);
            Assert.Equal(0.18, subscores.ShippingAddress);
            Assert.Equal(0.19, subscores.ShippingAddressDistanceToIPLocation);
            Assert.Equal(0.20, subscores.TimeOfDay);
        }
    }
#pragma warning restore 0618
}
