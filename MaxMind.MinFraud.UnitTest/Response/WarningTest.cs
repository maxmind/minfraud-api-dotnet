using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class WarningTest
    {
        [Test]
        public void TestWarning()
        {
            var code = "INVALID_INPUT";
            var msg = "Input invalid";

            Warning warning = new JObject
            {
                {"code", code},
                {"warning", msg},
                {"input_pointer", "/first/second"}
            }.ToObject<Warning>();

            Assert.AreEqual(code, warning.Code);
            Assert.AreEqual(msg, warning.Message);
            Assert.AreEqual("/first/second", warning.InputPointer);
        }
    }
}