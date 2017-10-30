using MaxMind.MinFraud.Response;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal static class AddressTestHelper
    {
        internal static void TestAddress(Address address)
        {
            Assert.True(address.IsInIPCountry);
            Assert.True(address.IsPostalInCity);
            Assert.Equal(100, address.DistanceToIPLocation);
            Assert.Equal(32.1, address.Longitude);
            Assert.Equal(43.1, address.Latitude);
        }
    }
}
