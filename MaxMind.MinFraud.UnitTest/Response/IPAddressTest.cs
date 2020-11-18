using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IPAddressTest
    {
        [Fact]
        public void TestIPAddress()
        {
            var time = "2015-04-19T12:59:23-01:00";

            var address = JsonSerializer.Deserialize<IPAddress>(
                $@"
                    {{
                        ""risk"": 99,
                        ""country"": {{""is_high_risk"": true}},
                        ""location"": {{""local_time"": ""{time}""}},
                        ""traits"": {{
                            ""is_anonymous"": true,
                            ""is_anonymous_vpn"": true,
                            ""is_hosting_provider"": true,
                            ""is_public_proxy"": true,
                            ""is_residential_proxy"": true,
                            ""is_tor_exit_node"": true
                         }}
                    }}
                ")!;

            Assert.Equal(99, address.Risk);
            Assert.Equal(time, address.Location.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
#pragma warning disable CS0618 // Type or member is obsolete
            Assert.True(address.Country.IsHighRisk);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(address.Traits.IsAnonymous);
            Assert.True(address.Traits.IsAnonymousVpn);
            Assert.True(address.Traits.IsHostingProvider);
            Assert.True(address.Traits.IsPublicProxy);
            Assert.True(address.Traits.IsResidentialProxy);
            Assert.True(address.Traits.IsTorExitNode);
        }
    }
}