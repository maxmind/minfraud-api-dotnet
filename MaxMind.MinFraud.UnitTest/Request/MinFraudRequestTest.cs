using MaxMind.MinFraud.Request;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class MinFraudRequestTest
    {
        [Fact]
        public void TestAccount()
        {
            var request = new Transaction { Account = new Account { UserId = "1" } };
            Assert.Equal("1", request.Account!.UserId);
        }

        [Fact]
        public void TestBilling()
        {
            var request = new Transaction { Billing = new Billing { Address = "add" } };
            Assert.Equal("add", request.Billing!.Address);
        }

        [Fact]
        public void TestCreditCard()
        {
            var request = new Transaction { CreditCard = new CreditCard { BankName = "name" } };
            Assert.Equal("name", request.CreditCard!.BankName);
        }

        [Fact]
        public void TestDevice()
        {
            var ip = IPAddress.Parse("1.1.1.1");
            var request = new Transaction { Device = new Device { IPAddress = ip } };
            Assert.Equal(ip, request.Device?.IPAddress);
        }

        [Fact]
        public void TestEmail()
        {
            var request = new Transaction { Email = new Email { Domain = "test.com" } };
            Assert.Equal("test.com", request.Email!.Domain);
        }

        [Fact]
        public void TestEvent()
        {
            var request = new Transaction { Event = new Event { ShopId = "1" } };
            Assert.Equal("1", request.Event!.ShopId);
        }

        [Fact]
        public void TestOrder()
        {
            var request = new Transaction { Order = new Order { AffiliateId = "af1" } };
            Assert.Equal("af1", request.Order!.AffiliateId);
        }

        [Fact]
        public void TestPayment()
        {
            var request = new Transaction { Payment = new Payment { DeclineCode = "d" } };
            Assert.Equal("d", request.Payment!.DeclineCode);
        }

        [Fact]
        public void TestShipping()
        {
            var request = new Transaction { Shipping = new Shipping { LastName = "l" } };
            Assert.Equal("l", request.Shipping!.LastName);
        }

        [Fact]
        public void TestShoppingCart()
        {
            var request = new Transaction
            {
                ShoppingCart = new List<ShoppingCartItem> { new ShoppingCartItem { ItemId = "1" } }
            };
            Assert.Equal("1", request.ShoppingCart![0].ItemId);
        }
    }
}
