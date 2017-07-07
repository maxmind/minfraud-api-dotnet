using System;
using System.Net;
using MaxMind.MinFraud.Request;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class DeviceTest
    {
        private IPAddress IP { get; } = IPAddress.Parse("1.1.1.1");

        [Fact]
        public void TestIPAddress()
        {
            var device = new Device(ipAddress: IP);
            Assert.Equal(IP, device.IPAddress);
        }

        [Fact]
        public void TestUserAgent()
        {
            var ua = "Mozila 5";
            var device = new Device(ipAddress: IP, userAgent: ua);
            Assert.Equal(ua, device.UserAgent);
        }

        [Fact]
        public void TestAcceptLanguage()
        {
            var al = "en-US";
            var device = new Device(IP, acceptLanguage: al);
            Assert.Equal(al, device.AcceptLanguage);
        }

        [Fact]
        public void TestSessionAge()
        {
            var device = new Device(IP, sessionAge: 3600);
            Assert.Equal(3600, device.SessionAge);
        }

        [Fact]
        public void TestSessionAgeIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Device(IP, sessionAge: -1));
        }

        [Fact]
        public void TestSessionId()
        {
            var device = new Device(IP, sessionId: "foo");
            Assert.Equal("foo", device.SessionId);
        }

        [Fact]
        public void TestSessionIdIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Device(IP, sessionId: new String('x', 256)));
        }
    }
}