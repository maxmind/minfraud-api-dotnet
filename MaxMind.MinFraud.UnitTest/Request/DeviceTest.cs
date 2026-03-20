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
            var device = new Device { IPAddress = ip };
            Assert.Equal(ip, device.IPAddress);
        }

        [Fact]
        public void TestUserAgent()
        {
            var ua = "Mozila 5";
            var device = new Device { UserAgent = ua };
            Assert.Equal(ua, device.UserAgent);
        }

        [Fact]
        public void TestAcceptLanguage()
        {
            var al = "en-US";
            var device = new Device { AcceptLanguage = al };
            Assert.Equal(al, device.AcceptLanguage);
        }

        [Fact]
        public void TestSessionAge()
        {
            var device = new Device { SessionAge = 3600 };
            Assert.Equal(3600, device.SessionAge);
        }

        [Fact]
        public void TestSessionAgeIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Device { SessionAge = -1 });
        }

        [Fact]
        public void TestSessionId()
        {
            var device = new Device { SessionId = "foo" };
            Assert.Equal("foo", device.SessionId);
        }

        [Fact]
        public void TestSessionIdIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Device { SessionId = new string('x', 256) });
        }

        [Fact]
        public void TestTrackingToken()
        {
            var token = "a]L@E*bnoqHa9&SBSwbB8X3#1E";
            var device = new Device { TrackingToken = token };
            Assert.Equal(token, device.TrackingToken);
        }
    }
}
