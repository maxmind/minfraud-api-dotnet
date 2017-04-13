using Newtonsoft.Json.Linq;
using Xunit;
using System;
using Device = MaxMind.MinFraud.Response.Disposition;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal class DispositionTest
    {
        [Fact]
        public void TestDisposition()
        {
            var disposition = new JObject
            {
                {"action", "manual_review"},
                {"reason", "custom_rule"},
            }.ToObject<Device>();

            Assert.Equal("manual_review", disposition.Action);
            Assert.Equal("custom_rule", disposition.Reason);
        }
    }
}