using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IssuerTest
    {
        [Test]
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

            Assert.AreEqual("Bank", issuer.Name);
            Assert.AreEqual(true, issuer.MatchesProvidedName);
            Assert.AreEqual(phone, issuer.PhoneNumber);
            Assert.AreEqual(true, issuer.MatchesProvidedPhoneNumber);
        }
    }
}