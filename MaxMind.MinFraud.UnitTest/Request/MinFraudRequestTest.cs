using System.Collections.Generic;
using System.Net;
using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class MinFraudRequestTest
    {
        private Device Device { get; } = new Device(IPAddress.Parse("1.1.1.1"));

        [Test]
        public void TestAccount()
        {
            var request = new MinFraudRequest(device: Device, account: new Account(userId: "1"));
            Assert.AreEqual("1", request.Account.UserId);
        }

        [Test]
        public void TestBilling()
        {
            var request = new MinFraudRequest(device: Device, billing: new Billing(address: "add"));
            Assert.AreEqual("add", request.Billing.Address);
        }

        [Test]
        public void TestCreditCard()
        {
            var request = new MinFraudRequest(device: Device, creditCard: new CreditCard(bankName: "name"));
            Assert.AreEqual("name", request.CreditCard.BankName);
        }

        [Test]
        public void TestDevice()
        {
            var request = new MinFraudRequest(device: Device);
            Assert.AreEqual(IPAddress.Parse("1.1.1.1"), request.Device.IPAddress);
        }

        [Test]
        public void TestEmail()
        {
            var request = new MinFraudRequest(device: Device, email: new Email(domain: "test.com"));
            Assert.AreEqual("test.com", request.Email.Domain);
        }

        [Test]
        public void TestEvent()
        {
            var request = new MinFraudRequest(device: Device, userEvent: new Event(shopId: "1"));
            Assert.AreEqual("1", request.Event.ShopId);
        }

        [Test]
        public void TestOrder()
        {
            var request = new MinFraudRequest(device: Device, order: new Order(affiliateId: "af1"));
            Assert.AreEqual("af1", request.Order.AffiliateId);
        }

        [Test]
        public void TestPayment()
        {
            var request = new MinFraudRequest(device: Device, payment: new Payment(declineCode: "d"));
            Assert.AreEqual("d", request.Payment.DeclineCode);
        }

        [Test]
        public void TestShipping()
        {
            var request = new MinFraudRequest(device: Device, shipping: new Shipping(lastName: "l"));
            Assert.AreEqual("l", request.Shipping.LastName);
        }

        [Test]
        public void TestShoppingCart()
        {
            var request = new MinFraudRequest(device: Device,
                shoppingCart: new List<ShoppingCartItem> {new ShoppingCartItem(itemId: "1")});
            Assert.AreEqual("1", request.ShoppingCart[0].ItemId);
        }
    }
}