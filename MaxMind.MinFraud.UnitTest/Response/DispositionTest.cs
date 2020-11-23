using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class DispositionTest
    {
        [Fact]
        public void TestDisposition()
        {
            var disposition = JsonSerializer.Deserialize<Disposition>(
                @"
                    {
                        ""action"": ""manual_review"",
                        ""reason"": ""custom_rule""
                    }
                ")!;

            Assert.Equal("manual_review", disposition.Action);
            Assert.Equal("custom_rule", disposition.Reason);
        }
    }
}