using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using Device = MaxMind.MinFraud.Response.Disposition;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal class DispositionTest
    {
        [Test]
        public void TestDisposition()
        {
            var disposition = new JObject
            {
                {"action", "manual_review"},
                {"reason", "custom_rule"},
            }.ToObject<Device>();

            Assert.AreEqual("manual_review", disposition.Action);
            Assert.AreEqual("custom_rule", disposition.Reason);
        }
    }
}