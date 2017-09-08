using MaxMind.MinFraud.Request;
using Xunit;
using System;

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
            var loc = new Billing(
                firstName: "frst"
            );
            Assert.Equal("frst", loc.FirstName);
        }

        [Fact]
        public void TestLastName()
        {
            var loc = new Billing(lastName: "last");
            Assert.Equal("last", loc.LastName);
        }

        [Fact]
        public void TestCompany()
        {
            var loc = new Billing(company: "company");
            Assert.Equal("company", loc.Company);
        }

        [Fact]
        public void TestAddress()
        {
            var loc = new Billing(address: "addr");
            Assert.Equal("addr", loc.Address);
        }

        [Fact]
        public void TestAddress2()
        {
            var loc = new Billing(address2: "addr2");
            Assert.Equal("addr2", loc.Address2);
        }

        [Fact]
        public void TestCity()
        {
            var loc = new Billing(city: "Pdx");
            Assert.Equal("Pdx", loc.City);
        }

        [Fact]
        public void TestRegion()
        {
            var loc = new Billing(region: "MN");
            Assert.Equal("MN", loc.Region);
        }

        [Fact]
        public void TestCountry()
        {
            var loc = new Billing(country: "US");
            Assert.Equal("US", loc.Country);
        }

        [Fact]
        public void TestCountryThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Billing(country: "USA"));
        }

        [Fact]
        public void TestCountryWithNumbers()
        {
            Assert.Throws<ArgumentException>(() => new Billing(country: "U1"));
        }

        [Fact]
        public void TestCountryInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Billing(country: "us"));
        }

        [Fact]
        public void TestPostal()
        {
            var loc = new Billing(postal: "03231");
            Assert.Equal("03231", loc.Postal);
        }

        [Fact]
        public void TestPhoneNumber()
        {
            var phone = "321-321-3213";
            var loc = new Billing(phoneNumber: phone);
            Assert.Equal(phone, loc.PhoneNumber);
        }

        [Fact]
        public void TestPhoneCountryCode()
        {
            var loc = new Billing(phoneCountryCode: "1");
            Assert.Equal("1", loc.PhoneCountryCode);
        }
    }
}