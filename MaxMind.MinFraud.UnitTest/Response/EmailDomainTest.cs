using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class EmailDomainTest
    {
        [Fact]
        public void TestEmailDomain()
        {
            var domain = JsonSerializer.Deserialize<EmailDomain>(
                @"{""first_seen"": ""2017-01-02""}")!;

            Assert.Equal("2017-01-02", domain.FirstSeen?.ToString("yyyy-MM-dd"));
        }
    }
}