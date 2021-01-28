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
                        ""risk_reasons"": [
                            {{
                                ""code"": ""ANONYMOUS_IP"",
                                ""reason"": ""The IP address belongs to an anonymous network. See /ip_address/traits for more details.""
                            }},
                            {{
                                ""code"": ""MINFRAUD_NETWORK_ACTIVITY"",
                                ""reason"": ""Suspicious activity has been seen on this IP address across minFraud customers.""
                            }}
                        ],
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
            Assert.Equal("ANONYMOUS_IP", address.RiskReasons[0].Code);
            Assert.Equal(
                "The IP address belongs to an anonymous network. See /ip_address/traits for more details.",
                address.RiskReasons[0].Reason
            );
            Assert.Equal("MINFRAUD_NETWORK_ACTIVITY", address.RiskReasons[1].Code);
            Assert.Equal(
                "Suspicious activity has been seen on this IP address across minFraud customers.",
                address.RiskReasons[1].Reason
            );
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