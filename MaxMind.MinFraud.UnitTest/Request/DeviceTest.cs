using MaxMind.MinFraud.Request;
using System;
using System.Net;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class DeviceTest
    {
        [Fact]
        public void TestIPAddress()
        {
            var ip = IPAddress.Parse("1.1.1.1");
            var device = new Device(ipAddress: ip);
            Assert.Equal(ip, device.IPAddress);
        }

        [Fact]
        public void TestUserAgent()
        {
            var ua = "Mozila 5";
            var device = new Device(userAgent: ua);
            Assert.Equal(ua, device.UserAgent);
        }

        [Fact]
        public void TestAcceptLanguage()
        {
            var al = "en-US";
            var device = new Device(acceptLanguage: al);
            Assert.Equal(al, device.AcceptLanguage);
        }

        [Fact]
        public void TestSessionAge()
        {
            var device = new Device(sessionAge: 3600);
            Assert.Equal(3600, device.SessionAge);
        }

        [Fact]
        public void TestSessionAgeIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Device(sessionAge: -1));
        }

        [Fact]
        public void TestSessionId()
        {
            var device = new Device(sessionId: "foo");
            Assert.Equal("foo", device.SessionId);
        }

        [Fact]
        public void TestSessionIdIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Device(sessionId: new string('x', 256)));
        }
    }
}