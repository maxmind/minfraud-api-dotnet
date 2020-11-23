using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class WarningTest
    {
        [Fact]
        public void TestWarning()
        {
            var code = "INVALID_INPUT";
            var msg = "Input invalid";

            var warning = JsonSerializer.Deserialize<Warning>(
                $@"
                    {{
                        ""code"": ""{code}"",
                        ""warning"": ""{msg}"",
                        ""input_pointer"": ""/first/second""
                    }}
                ")!;

            Assert.Equal(code, warning.Code);
            Assert.Equal(msg, warning.Message);
            Assert.Equal("/first/second", warning.InputPointer);
        }
    }
}