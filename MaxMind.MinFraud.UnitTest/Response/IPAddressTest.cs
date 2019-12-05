using MaxMind.MinFraud.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class IPAddressTest
    {
        [Fact]
        public void TestIPAddress()
        {
            var time = "2015-04-19T12:59:23-01:00";

            var address = new JObject
            {
                {"risk", 99},
                {"country", new JObject {{"is_high_risk", true}}},
                {"location", new JObject {{"local_time", time}}},
                {
                    "traits", new JObject
                    {
                        {"is_anonymous", true},
                        {"is_anonymous_vpn", true},
                        {"is_hosting_provider", true},
                        {"is_public_proxy", true},
                        {"is_tor_exit_node", true},
                    }
                }
            }.ToObject<IPAddress>();

            Assert.Equal(99, address.Risk);
            Assert.Equal(time, address.Location.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
#pragma warning disable CS0618 // Type or member is obsolete
            Assert.True(address.Country.IsHighRisk);
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(address.Traits.IsAnonymous);
            Assert.True(address.Traits.IsAnonymousVpn);
            Assert.True(address.Traits.IsHostingProvider);
            Assert.True(address.Traits.IsPublicProxy);
            Assert.True(address.Traits.IsTorExitNode);
        }
    }
}