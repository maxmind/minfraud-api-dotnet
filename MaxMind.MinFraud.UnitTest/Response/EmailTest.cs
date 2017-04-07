using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailTest
    {
        [Fact]
        public void TestEmail()
        {
            var email = new JObject
            {
                {"is_free", true},
                {"is_high_risk", true}
            }.ToObject<Email>();

            Assert.True(email.IsFree);
            Assert.True(email.IsHighRisk);
        }
    }
}