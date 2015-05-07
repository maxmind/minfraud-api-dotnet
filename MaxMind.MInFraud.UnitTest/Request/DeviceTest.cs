using System.Net;
using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class DeviceTest
    {
        private IPAddress IP { get; } = IPAddress.Parse("1.1.1.1");

        [Test]
        public void TestIPAddress()
        {
            var device = new Device(ipAddress: IP);
            Assert.AreEqual(IP, device.IPAddress);
        }

        [Test]
        public void TestUserAgent()
        {
            var ua = "Mozila 5";
            var device = new Device(ipAddress: IP, userAgent: ua);
            Assert.AreEqual(ua, device.UserAgent);
        }

        [Test]
        public void TestAcceptLanguage()
        {
            var al = "en-US";
            var device = new Device(IP, acceptLanguage: al);
            Assert.AreEqual(al, device.AcceptLanguage);
        }
    }
}