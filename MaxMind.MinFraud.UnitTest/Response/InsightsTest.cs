using MaxMind.MinFraud.Response;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class InsightsTest
    {
        [Fact]
        public void TestInsights()
        {
            var id = "b643d445-18b2-4b9d-bad4-c9c4366e402a";
            var insights = JsonSerializer.Deserialize<Insights>(
                   $$$"""
                   {
                       "id": "{{{id}}}",
                       "ip_address": {"country": {"iso_code": "US"}},
                       "credit_card": {"is_business": true, "is_prepaid": true},
                       "device": {"id": "{{{id}}}"},
                       "disposition": {"action": "accept"},
                       "email":
                           {
                               "domain": { "first_seen": "2014-02-03"},
                               "is_free": true
                           },
                       "shipping_address": {"is_in_ip_country": true},
                       "shipping_phone": {"is_voip": true},
                       "billing_address": {"is_in_ip_country": true},
                       "billing_phone": {"is_voip": false},
                       "funds_remaining": 1.20,
                       "queries_remaining": 123,
                       "risk_score": 0.01,
                       "warnings": [{"code": "INVALID_INPUT"}]
                   }
                   """)!;

            Assert.Equal("2014-02-03", insights.Email.Domain.FirstSeen?.ToString("yyyy-MM-dd"));
            Assert.Equal("US", insights.IPAddress.Country.IsoCode);
            Assert.True(insights.CreditCard.IsBusiness);
            Assert.True(insights.CreditCard.IsPrepaid);
            Assert.Equal(id, insights.Device.Id.ToString());
            Assert.Equal("accept", insights.Disposition.Action);
            Assert.True(insights.Email.IsFree);
            Assert.True(insights.ShippingAddress.IsInIPCountry);
            Assert.True(insights.ShippingPhone.IsVoip);
            Assert.True(insights.BillingAddress.IsInIPCountry);
            Assert.False(insights.BillingPhone.IsVoip);
            Assert.Equal(id, insights.Id.ToString());
            Assert.Equal(1.20m, insights.FundsRemaining);
            Assert.Equal(123, insights.QueriesRemaining);
            Assert.Equal(0.01, insights.RiskScore);
            Assert.Equal("INVALID_INPUT", insights.Warnings[0].Code);
        }

        [Fact]
        public void TestIPAddressAnonymizer()
        {
            var insights = JsonSerializer.Deserialize<Insights>(
                """
                {
                    "id": "b643d445-18b2-4b9d-bad4-c9c4366e402a",
                    "ip_address": {
                        "country": {"iso_code": "US"},
                        "anonymizer": {
                            "confidence": 85,
                            "is_anonymous": true,
                            "is_anonymous_vpn": true,
                            "is_hosting_provider": false,
                            "is_public_proxy": false,
                            "is_residential_proxy": true,
                            "is_tor_exit_node": false,
                            "network_last_seen": "2025-01-15",
                            "provider_name": "TestVPN"
                        }
                    },
                    "risk_score": 0.5
                }
                """)!;

            var anonymizer = insights.IPAddress.Anonymizer;
            Assert.Equal(85, anonymizer.Confidence);
            Assert.True(anonymizer.IsAnonymous);
            Assert.True(anonymizer.IsAnonymousVpn);
            Assert.False(anonymizer.IsHostingProvider);
            Assert.False(anonymizer.IsPublicProxy);
            Assert.True(anonymizer.IsResidentialProxy);
            Assert.False(anonymizer.IsTorExitNode);
#if NET6_0_OR_GREATER
            Assert.Equal("2025-01-15", anonymizer.NetworkLastSeen?.ToString("yyyy-MM-dd"));
#endif
            Assert.Equal("TestVPN", anonymizer.ProviderName);
        }

        [Fact]
        public void TestIPAddressAnonymizerEmpty()
        {
            var insights = JsonSerializer.Deserialize<Insights>(
                """
                {
                    "id": "b643d445-18b2-4b9d-bad4-c9c4366e402a",
                    "ip_address": {
                        "country": {"iso_code": "US"},
                        "anonymizer": {
                            "is_anonymous": false,
                            "is_anonymous_vpn": false,
                            "is_hosting_provider": false,
                            "is_public_proxy": false,
                            "is_residential_proxy": false,
                            "is_tor_exit_node": false
                        }
                    },
                    "risk_score": 0.01
                }
                """)!;

            var anonymizer = insights.IPAddress.Anonymizer;
            Assert.Null(anonymizer.Confidence);
            Assert.False(anonymizer.IsAnonymous);
            Assert.False(anonymizer.IsAnonymousVpn);
            Assert.False(anonymizer.IsHostingProvider);
            Assert.False(anonymizer.IsPublicProxy);
            Assert.False(anonymizer.IsResidentialProxy);
            Assert.False(anonymizer.IsTorExitNode);
#if NET6_0_OR_GREATER
            Assert.Null(anonymizer.NetworkLastSeen);
#endif
            Assert.Null(anonymizer.ProviderName);
        }
    }
}
