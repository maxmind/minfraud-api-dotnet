using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IssuerTest
    {
        [Fact]
        public void TestIssuer()
        {
            var phone = "132-342-2131";

            var issuer = new JObject
            {
                {"name", "Bank"},
                {"matches_provided_name", true},
                {"phone_number", phone},
                {"matches_provided_phone_number", true}
            }.ToObject<Issuer>();

            Assert.Equal("Bank", issuer.Name);
            Assert.True(issuer.MatchesProvidedName);
            Assert.Equal(phone, issuer.PhoneNumber);
            Assert.True(issuer.MatchesProvidedPhoneNumber);
        }
    }
}
