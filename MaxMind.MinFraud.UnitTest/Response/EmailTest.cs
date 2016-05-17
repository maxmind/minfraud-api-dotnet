using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailTest
    {
        [Test]
        public void TestEmail()
        {
            var email = new JObject
            {
                {"is_free", true},
                {"is_high_risk", true}
            }.ToObject<Email>();

            Assert.IsTrue(email.IsFree);
            Assert.IsTrue(email.IsHighRisk);
        }
    }
}