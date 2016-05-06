using MaxMind.MinFraud.Request;
using NUnit.Framework;
using System;

namespace MaxMind.MinFraud.UnitTest.Request
{
    // This code is identical to code in ShippingTest. I couldn't
    // come up with a good way to share it due to to specifiy arguments
    // to new() for a variable type. There are ways around this generally
    // but most defeat the purpose of the Tests.
    public class BillingTest
    {
        [Test]
        public void TestFirstName()
        {
            var loc = new Billing(
                firstName: "frst"
                );
            Assert.AreEqual("frst", loc.FirstName);
        }

        [Test]
        public void TestLastName()
        {
            var loc = new Billing(lastName: "last");
            Assert.AreEqual("last", loc.LastName);
        }

        [Test]
        public void TestCompany()
        {
            var loc = new Billing(company: "company");
            Assert.AreEqual("company", loc.Company);
        }

        [Test]
        public void TestAddress()
        {
            var loc = new Billing(address: "addr");
            Assert.AreEqual("addr", loc.Address);
        }

        [Test]
        public void TestAddress2()
        {
            var loc = new Billing(address2: "addr2");
            Assert.AreEqual("addr2", loc.Address2);
        }

        [Test]
        public void TestCity()
        {
            var loc = new Billing(city: "Pdx");
            Assert.AreEqual("Pdx", loc.City);
        }

        [Test]
        public void TestRegion()
        {
            var loc = new Billing(region: "MN");
            Assert.AreEqual("MN", loc.Region);
        }

        [Test]
        public void TestCountry()
        {
            var loc = new Billing(country: "US");
            Assert.AreEqual("US", loc.Country);
        }

        [Test]
        public void TestCountryThatIsTooLong()
        {
            Assert.That(() => new Billing(country: "USA"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCountryWithNumbers()
        {
            Assert.That(() => new Billing(country: "U1"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCountryInWrongCase()
        {
            Assert.That(() => new Billing(country: "us"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestPostal()
        {
            var loc = new Billing(postal: "03231");
            Assert.AreEqual("03231", loc.Postal);
        }

        [Test]
        public void TestPhoneNumber()
        {
            string phone = "321-321-3213";
            var loc = new Billing(phoneNumber: phone);
            Assert.AreEqual(phone, loc.PhoneNumber);
        }

        [Test]
        public void TestPhoneCountryCode()
        {
            var loc = new Billing(phoneCountryCode: "1");
            Assert.AreEqual("1", loc.PhoneCountryCode);
        }
    }
}