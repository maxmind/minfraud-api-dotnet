using System.Collections.Generic;
using System.Net;
using MaxMind.MinFraud.Request;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class MinFraudRequestTest
    {
        private Device Device { get; } = new Device(IPAddress.Parse("1.1.1.1"));

        [Fact]
        public void TestAccount()
        {
            var request = new Transaction(device: Device, account: new Account(userId: "1"));
            Assert.Equal("1", request.Account!.UserId);
        }

        [Fact]
        public void TestBilling()
        {
            var request = new Transaction(device: Device, billing: new Billing(address: "add"));
            Assert.Equal("add", request.Billing!.Address);
        }

        [Fact]
        public void TestCreditCard()
        {
            var request = new Transaction(device: Device, creditCard: new CreditCard(bankName: "name"));
            Assert.Equal("name", request.CreditCard!.BankName);
        }

        [Fact]
        public void TestDevice()
        {
            var request = new Transaction(device: Device);
            Assert.Equal(IPAddress.Parse("1.1.1.1"), request.Device.IPAddress);
        }

        [Fact]
        public void TestEmail()
        {
            var request = new Transaction(device: Device, email: new Email(domain: "test.com"));
            Assert.Equal("test.com", request.Email!.Domain);
        }

        [Fact]
        public void TestEvent()
        {
            var request = new Transaction(device: Device, userEvent: new Event(shopId: "1"));
            Assert.Equal("1", request.Event!.ShopId);
        }

        [Fact]
        public void TestOrder()
        {
            var request = new Transaction(device: Device, order: new Order(affiliateId: "af1"));
            Assert.Equal("af1", request.Order!.AffiliateId);
        }

        [Fact]
        public void TestPayment()
        {
            var request = new Transaction(device: Device, payment: new Payment(declineCode: "d"));
            Assert.Equal("d", request.Payment!.DeclineCode);
        }

        [Fact]
        public void TestShipping()
        {
            var request = new Transaction(device: Device, shipping: new Shipping(lastName: "l"));
            Assert.Equal("l", request.Shipping!.LastName);
        }

        [Fact]
        public void TestShoppingCart()
        {
            var request = new Transaction(device: Device,
                shoppingCart: new List<ShoppingCartItem> {new ShoppingCartItem(itemId: "1")});
            Assert.Equal("1", request.ShoppingCart![0].ItemId);
        }
    }
}