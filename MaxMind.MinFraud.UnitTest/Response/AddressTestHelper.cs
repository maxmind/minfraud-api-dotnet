using MaxMind.MinFraud.Response;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal static class AddressTestHelper
    {
        internal static void TestAddress(Address address)
        {
            Assert.AreEqual(true, address.IsInIpCountry);
            Assert.AreEqual(true, address.IsPostalInCity);
            Assert.AreEqual(100, address.DistanceToIpLocation);
            Assert.AreEqual(32.1, address.Longitude);
            Assert.AreEqual(43.1, address.Latitude);
        }
    }
}