#region

using System;
using MaxMind.MinFraud.Exception;
using MaxMind.MinFraud.Request;
using NUnit.Framework;

#endregion

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class ShippingTest
    {
        // Same as code in BillingTest
        [Test]
        public void TestFirstName()
        {
            var loc = new Shipping(
                firstName: "frst"
                );
            Assert.AreEqual("frst", loc.FirstName);
        }

        [Test]
        public void TestLastName()
        {
            var loc = new Shipping(lastName: "last");
            Assert.AreEqual("last", loc.LastName);
        }

        [Test]
        public void TestCompany()
        {
            var loc = new Shipping(company: "company");
            Assert.AreEqual("company", loc.Company);
        }

        [Test]
        public void TestAddress()
        {
            var loc = new Shipping(address: "addr");
            Assert.AreEqual("addr", loc.Address);
        }

        [Test]
        public void TestAddress2()
        {
            var loc = new Shipping(address2: "addr2");
            Assert.AreEqual("addr2", loc.Address2);
        }

        [Test]
        public void TestCity()
        {
            var loc = new Shipping(city: "Pdx");
            Assert.AreEqual("Pdx", loc.City);
        }

        [Test]
        public void TestRegion()
        {
            var loc = new Shipping(region: "MN");
            Assert.AreEqual("MN", loc.Region);
        }

        [Test]
        public void TestCountry()
        {
            var loc = new Shipping(country: "US");
            Assert.AreEqual("US", loc.Country);
        }

        [Test]
        public void TestCountryThatIsTooLong()
        {
            Assert.That(() => new Shipping(country: "USA"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCountryWithNumbers()
        {
            Assert.That(() => new Shipping(country: "U1"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCountryInWrongCase()
        {
            Assert.That(() => new Shipping(country: "us"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestPostal()
        {
            var loc = new Shipping(postal: "03231");
            Assert.AreEqual("03231", loc.Postal);
        }

        [Test]
        public void TestPhoneNumber()
        {
            var phone = "321-321-3213";
            var loc = new Shipping(phoneNumber: phone);
            Assert.AreEqual(phone, loc.PhoneNumber);
        }

        [Test]
        public void TestPhoneCountryCode()
        {
            var loc = new Shipping(phoneCountryCode: "1");
            Assert.AreEqual("1", loc.PhoneCountryCode);
        }

        // End shared code

        [Test]
        public void TestDeliverySpeed()
        {
            var loc = new Shipping(deliverySpeed: ShippingDeliverySpeed.Expedited);
            Assert.AreEqual(ShippingDeliverySpeed.Expedited, loc.DeliverySpeed);
        }
    }
}