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
                """{"first_seen": "2017-01-02"}""")!;

            Assert.Equal("2017-01-02", domain.FirstSeen?.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public void TestEmailDomainWithAllFields()
        {
            var json = """
                {
                    "classification": "business",
                    "first_seen": "2019-01-20",
                    "risk": 15.5,
                    "volume": 1234.56,
                    "visit": {
                        "has_redirect": true,
                        "last_visited_on": "2025-11-15",
                        "status": "live"
                    }
                }
                """;

            var domain = JsonSerializer.Deserialize<EmailDomain>(json)!;

            Assert.Equal(DomainClassification.Business, domain.Classification);
            Assert.Equal("2019-01-20", domain.FirstSeen?.ToString("yyyy-MM-dd"));
            Assert.Equal(15.5, domain.Risk);
            Assert.Equal(1234.56, domain.Volume);
            Assert.NotNull(domain.Visit);
            Assert.True(domain.Visit.HasRedirect);
            Assert.Equal("2025-11-15", domain.Visit.LastVisitedOn?.ToString("yyyy-MM-dd"));
            Assert.Equal(DomainVisitStatus.Live, domain.Visit.Status);
        }

        [Fact]
        public void TestEmailDomainClassifications()
        {
            var json = """{"classification": "education"}""";
            var domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainClassification.Education, domain.Classification);

            json = """{"classification": "government"}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainClassification.Government, domain.Classification);

            json = """{"classification": "isp_email"}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainClassification.IspEmail, domain.Classification);
        }

        [Fact]
        public void TestEmailDomainVisitStatuses()
        {
            var json = """{"visit": {"status": "dns_error"}}""";
            var domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainVisitStatus.DnsError, domain.Visit?.Status);

            json = """{"visit": {"status": "network_error"}}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainVisitStatus.NetworkError, domain.Visit?.Status);

            json = """{"visit": {"status": "http_error"}}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainVisitStatus.HttpError, domain.Visit?.Status);

            json = """{"visit": {"status": "parked"}}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainVisitStatus.Parked, domain.Visit?.Status);

            json = """{"visit": {"status": "pre_development"}}""";
            domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(DomainVisitStatus.PreDevelopment, domain.Visit?.Status);
        }

        [Fact]
        public void TestEmailDomainWithPartialFields()
        {
            var json = """{"risk": 45.2}""";
            var domain = JsonSerializer.Deserialize<EmailDomain>(json)!;
            Assert.Equal(45.2, domain.Risk);
            Assert.Null(domain.Classification);
            Assert.Null(domain.FirstSeen);
            Assert.Null(domain.Volume);
            Assert.Null(domain.Visit);
        }

        [Fact]
        public void TestEmailDomainWithUnknownEnumValues()
        {
            // Test that unknown enum values are handled gracefully for forward compatibility
            var json = """
                {
                    "classification": "unknown_future_value",
                    "risk": 25.0,
                    "visit": {
                        "status": "future_status_type"
                    }
                }
                """;

            // Should deserialize successfully with unknown enum values as null
            var domain = JsonSerializer.Deserialize<EmailDomain>(json)!;

            Assert.Null(domain.Classification); // Unknown value treated as null
            Assert.Equal(25.0, domain.Risk); // Other fields still populated
            Assert.NotNull(domain.Visit); // Visit object exists
            Assert.Null(domain.Visit.Status); // Unknown status treated as null
        }
    }
}