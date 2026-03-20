#region

using MaxMind.MinFraud.Request;
using System;
using Xunit;

#endregion

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class ShippingTest
    {
        // Same as code in BillingTest
        [Fact]
        public void TestFirstName()
        {
            var loc = new Shipping
            {
                FirstName = "frst"
            };
            Assert.Equal("frst", loc.FirstName);
        }

        [Fact]
        public void TestLastName()
        {
            var loc = new Shipping { LastName = "last" };
            Assert.Equal("last", loc.LastName);
        }

        [Fact]
        public void TestCompany()
        {
            var loc = new Shipping { Company = "company" };
            Assert.Equal("company", loc.Company);
        }

        [Fact]
        public void TestAddress()
        {
            var loc = new Shipping { Address = "addr" };
            Assert.Equal("addr", loc.Address);
        }

        [Fact]
        public void TestAddress2()
        {
            var loc = new Shipping { Address2 = "addr2" };
            Assert.Equal("addr2", loc.Address2);
        }

        [Fact]
        public void TestCity()
        {
            var loc = new Shipping { City = "Pdx" };
            Assert.Equal("Pdx", loc.City);
        }

        [Fact]
        public void TestRegion()
        {
            var loc = new Shipping { Region = "MN" };
            Assert.Equal("MN", loc.Region);
        }

        [Fact]
        public void TestCountry()
        {
            var loc = new Shipping { Country = "US" };
            Assert.Equal("US", loc.Country);
        }

        [Fact]
        public void TestCountryThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "USA" });
        }

        [Fact]
        public void TestCountryWithNumbers()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "U1" });
        }

        [Fact]
        public void TestCountryInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "us" });
        }

        [Fact]
        public void TestPostal()
        {
            var loc = new Shipping { Postal = "03231" };
            Assert.Equal("03231", loc.Postal);
        }

        [Fact]
        public void TestPhoneNumber()
        {
            var phone = "321-321-3213";
            var loc = new Shipping { PhoneNumber = phone };
            Assert.Equal(phone, loc.PhoneNumber);
        }

        [Fact]
        public void TestPhoneCountryCode()
        {
            var loc = new Shipping { PhoneCountryCode = "1" };
            Assert.Equal("1", loc.PhoneCountryCode);
        }

        // End shared code

        [Fact]
        public void TestDeliverySpeed()
        {
            var loc = new Shipping { DeliverySpeed = ShippingDeliverySpeed.Expedited };
            Assert.Equal(ShippingDeliverySpeed.Expedited, loc.DeliverySpeed);
        }
    }
}
