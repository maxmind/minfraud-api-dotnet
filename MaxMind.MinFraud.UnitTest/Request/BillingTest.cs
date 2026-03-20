using MaxMind.MinFraud.Request;
using System;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    // This code is identical to code in ShippingTest. I couldn't
    // come up with a good way to share it due to to specifiy arguments
    // to new() for a variable type. There are ways around this generally
    // but most defeat the purpose of the Tests.
    public class BillingTest
    {
        [Fact]
        public void TestFirstName()
        {
            var loc = new Billing
            {
                FirstName = "frst"
            };
            Assert.Equal("frst", loc.FirstName);
        }

        [Fact]
        public void TestLastName()
        {
            var loc = new Billing { LastName = "last" };
            Assert.Equal("last", loc.LastName);
        }

        [Fact]
        public void TestCompany()
        {
            var loc = new Billing { Company = "company" };
            Assert.Equal("company", loc.Company);
        }

        [Fact]
        public void TestAddress()
        {
            var loc = new Billing { Address = "addr" };
            Assert.Equal("addr", loc.Address);
        }

        [Fact]
        public void TestAddress2()
        {
            var loc = new Billing { Address2 = "addr2" };
            Assert.Equal("addr2", loc.Address2);
        }

        [Fact]
        public void TestCity()
        {
            var loc = new Billing { City = "Pdx" };
            Assert.Equal("Pdx", loc.City);
        }

        [Fact]
        public void TestRegion()
        {
            var loc = new Billing { Region = "MN" };
            Assert.Equal("MN", loc.Region);
        }

        [Fact]
        public void TestCountry()
        {
            var loc = new Billing { Country = "US" };
            Assert.Equal("US", loc.Country);
        }

        [Fact]
        public void TestCountryThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Billing { Country = "USA" });
        }

        [Fact]
        public void TestCountryWithNumbers()
        {
            Assert.Throws<ArgumentException>(() => new Billing { Country = "U1" });
        }

        [Fact]
        public void TestCountryInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Billing { Country = "us" });
        }

        [Fact]
        public void TestPostal()
        {
            var loc = new Billing { Postal = "03231" };
            Assert.Equal("03231", loc.Postal);
        }

        [Fact]
        public void TestPhoneNumber()
        {
            var phone = "321-321-3213";
            var loc = new Billing { PhoneNumber = phone };
            Assert.Equal(phone, loc.PhoneNumber);
        }

        [Fact]
        public void TestPhoneCountryCode()
        {
            var loc = new Billing { PhoneCountryCode = "1" };
            Assert.Equal("1", loc.PhoneCountryCode);
        }
    }
}
