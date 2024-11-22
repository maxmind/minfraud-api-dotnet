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
    }
}
