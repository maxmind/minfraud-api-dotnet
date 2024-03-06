using MaxMind.MinFraud.Request;
using System;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class EmailTest
    {
        [Fact]
        public void TestAddress()
        {
            var address = "test@maxmind.com";
            var domain = "maxmind.com";

            var email = new Email(address: address);
            Assert.Equal(address, email.Address);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", email.AddressMD5);
            Assert.Equal(domain, email.Domain);

            var json = JsonSerializer.Serialize(email);
            var comparer = new JsonElementComparer();
            Assert.True(
                comparer.JsonEquals(
                    JsonDocument.Parse(
                        $@"
                        {{
                            ""address"": ""{address}"",
                            ""domain"": ""{domain}""
                        }}
                        "),
                    JsonDocument.Parse(json)
                ),
                json
            );
        }

        [Fact]
        public void TestAddressWithHashing()
        {
            var address = "test@maxmind.com";
            var md5 = "977577b140bfb7c516e4746204fbdb01";
            var domain = "maxmind.com";

            var email = new Email(address: address, hashAddress: true);
            Assert.Equal(address, email.Address);
            Assert.Equal(md5, email.AddressMD5);
            Assert.Equal("maxmind.com", email.Domain);

            var json = JsonSerializer.Serialize(email);
            var comparer = new JsonElementComparer();
            Assert.True(
                comparer.JsonEquals(
                    JsonDocument.Parse(
                        $@"
                        {{
                            ""address"": ""{md5}"",
                            ""domain"": ""{domain}""
                        }}
                        "),
                    JsonDocument.Parse(json)
                )
            );
        }

        [Fact]
        public void TestNormalizing()
        {
            Email e = new Email(address: "test@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "  Test@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test+alias@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test+007+008@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test+@maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test@maxmind.com.", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal("maxmind.com.", e.Domain);

            e = new Email(address: "+@maxmind.com", hashAddress: true);
            Assert.Equal("aa57884e48f0dda9fc6f4cb2bffb1dd2", e.AddressMD5);
            Assert.Equal("maxmind.com", e.Domain);

            e = new Email(address: "Test@ maxmind.com", hashAddress: true);
            Assert.Equal("977577b140bfb7c516e4746204fbdb01", e.AddressMD5);
            Assert.Equal(" maxmind.com", e.Domain);

            e = new Email(address: "Test+foo@yahoo.com", hashAddress: true);
            Assert.Equal("a5f830c699fd71ad653aa59fa688c6d9", e.AddressMD5);
            Assert.Equal("yahoo.com", e.Domain);

            e = new Email(address: "Test-foo@yahoo.com", hashAddress: true);
            Assert.Equal("88e478531ab3bc303f1b5da82c2e9bbb", e.AddressMD5);
            Assert.Equal("yahoo.com", e.Domain);

            e = new Email(address: "Test-foo-foo2@yahoo.com", hashAddress: true);
            Assert.Equal("88e478531ab3bc303f1b5da82c2e9bbb", e.AddressMD5);
            Assert.Equal("yahoo.com", e.Domain);

            e = new Email(address: "Test-foo@gmail.com", hashAddress: true);
            Assert.Equal("6f3ff986fa5e830dbbf08a942777a17c", e.AddressMD5);
            Assert.Equal("gmail.com", e.Domain);

            e = new Email(address: "test@gmail.com", hashAddress: true);
            Assert.Equal("1aedb8d9dc4751e229a335e371db8058", e.AddressMD5);
            Assert.Equal("gmail.com", e.Domain);

            e = new Email(address: "test@gamil.com", hashAddress: true);
            Assert.Equal("1aedb8d9dc4751e229a335e371db8058", e.AddressMD5);
            Assert.Equal("gamil.com", e.Domain);

            e = new Email(address: "test@bücher.com", hashAddress: true);
            Assert.Equal("24948acabac551360cd510d5e5e2b464", e.AddressMD5);
            Assert.Equal("bücher.com", e.Domain);

            e = new Email(address: "Test+alias@Bücher.com", hashAddress: true);
            Assert.Equal("24948acabac551360cd510d5e5e2b464", e.AddressMD5);
            Assert.Equal("Bücher.com", e.Domain);

            e = new Email(address: "test@", hashAddress: true);
            Assert.Equal("246a848af2f8394e3adbc738dbe43720", e.AddressMD5);
            Assert.Equal("", e.Domain);

            e = new Email(address: "foo@googlemail.com", hashAddress: true);
            Assert.Equal("6c0fbec2cc554c35c3d2b8b51840b49d", e.AddressMD5);
            Assert.Equal("googlemail.com", e.Domain);

            e = new Email(address: "foo.bar@gmail.com", hashAddress: true);
            Assert.Equal("726f7c3769f32d3da4656ea906d02adb", e.AddressMD5);
            Assert.Equal("gmail.com", e.Domain);

            e = new Email(address: "alias@user.fastmail.com", hashAddress: true);
            Assert.Equal("2dc11f44b436d1bc4ecfd4806e469d33", e.AddressMD5);
            Assert.Equal("user.fastmail.com", e.Domain);

            e = new Email(address: "foo-bar@ymail.com", hashAddress: true);
            Assert.Equal("fead35da88f8414ec0414ef5f25d49c8", e.AddressMD5);
            Assert.Equal("ymail.com", e.Domain);

            e = new Email(address: "foo@example.com.com", hashAddress: true);
            Assert.Equal("b48def645758b95537d4424c84d1a9ff", e.AddressMD5);
            Assert.Equal("example.com.com", e.Domain);

            e = new Email(address: "foo@example.comfoo", hashAddress: true);
            Assert.Equal("b48def645758b95537d4424c84d1a9ff", e.AddressMD5);
            Assert.Equal("example.comfoo", e.Domain);
        }

        [Fact]
        public void TestInvalidAddress()
        {
            Assert.Throws<ArgumentException>(() => new Email(address: "no-domain"));
        }

        [Fact]
        public void TestDomain()
        {
            var domain = "domain.com";
            var email = new Email(domain: domain);
            Assert.Equal(domain, email.Domain);
        }

        [Fact]
        public void TestInvalidDomain()
        {
            Assert.Throws<ArgumentException>(() => new Email(domain: " domain.com"));
        }
    }
}