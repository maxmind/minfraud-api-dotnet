using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class CreditCardTest
    {
        [Fact]
        public void TestCreditCard()
        {
            var cc = JsonSerializer.Deserialize<CreditCard>(
                """
                {
                    "issuer": {"name": "Bank"},
                    "brand": "Visa",
                    "country": "US",
                    "is_business": true,
                    "is_issued_in_billing_address_country":  true,
                    "is_prepaid": true,
                    "is_virtual": true,
                    "type": "credit"
                }
                """)!;

            Assert.Equal("Bank", cc.Issuer.Name);
            Assert.Equal("US", cc.Country);
            Assert.True(cc.IsBusiness);
            Assert.True(cc.IsPrepaid);
            Assert.True(cc.IsIssuedInBillingAddressCountry);
            Assert.True(cc.IsVirtual);
            Assert.Equal("Visa", cc.Brand);
            Assert.Equal("credit", cc.Type);
        }
    }
}
