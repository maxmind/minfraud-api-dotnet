using MaxMind.MinFraud.Request;
using NUnit.Framework;
using System;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class EmailTest
    {
        [Test]
        public void TestAddress()
        {
            var address = "test@maxmind.com";
            var email = new Email(address: address);
            Assert.AreEqual(address, email.Address);
            Assert.AreEqual("977577b140bfb7c516e4746204fbdb01", email.AddressMD5);
            Assert.AreEqual("maxmind.com", email.Domain);
        }

        [Test]
        public void TestDomain()
        {
            var domain = "domain.com";
            var email = new Email(domain: domain);
            Assert.AreEqual(domain, email.Domain);
        }

        [Test]
        public void TestInvalidDomain()
        {
            Assert.That(() => new Email(domain: " domain.com"), Throws.TypeOf<ArgumentException>());
        }
    }
}