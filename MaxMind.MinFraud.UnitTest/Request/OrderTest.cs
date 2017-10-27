using MaxMind.MinFraud.Request;
using Xunit;
using System;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class OrderTest
    {
        [Fact]
        public void TestAmount()
        {
            var order = new Order(amount: 1.1m);
            Assert.Equal(1.1m, order.Amount);
        }

        [Fact]
        public void TestCurrency()
        {
            var order = new Order(currency: "USD");
            Assert.Equal("USD", order.Currency);
        }

        [Fact]
        public void TestCurrencyWithDigits()
        {
            Assert.Throws<ArgumentException>(() => new Order(currency: "US1"));
        }

        [Fact]
        public void TestCurrencyThatIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new Order(currency: "US"));
        }

        [Fact]
        public void TestCurrencyThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Order(currency: "USDE"));
        }

        [Fact]
        public void TestCurrencyInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Order(currency: "usd"));
        }

        [Fact]
        public void TestDiscountCode()
        {
            var order = new Order(discountCode: "dsc");
            Assert.Equal("dsc", order.DiscountCode);
        }

        [Fact]
        public void TestAffiliateId()
        {
            var order = new Order(affiliateId: "af");
            Assert.Equal("af", order.AffiliateId);
        }

        [Fact]
        public void TestSubaffiliateId()
        {
            var order = new Order(subaffiliateId: "saf");
            Assert.Equal("saf", order.SubaffiliateId);
        }

        [Fact]
        public void TestReferrerUri()
        {
            var uri = new Uri("http://www.mm.com/");
            var order = new Order(referrerUri: uri);
            Assert.Equal(uri, order.ReferrerUri);
        }

        [Fact]
        public void TestIsGift()
        {
            var order = new Order(isGift: true);
            Assert.True(order.IsGift);
        }

        [Fact]
        public void TestHasGiftMessage()
        {
            var order = new Order(hasGiftMessage: true);
            Assert.True(order.HasGiftMessage);
        }
    }
}
