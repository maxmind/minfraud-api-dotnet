using MaxMind.MinFraud.Request;
using System;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class OrderTest
    {
        [Fact]
        public void TestAmount()
        {
            var order = new Order { Amount = 1.1m };
            Assert.Equal(1.1m, order.Amount);
        }

        [Fact]
        public void TestCurrency()
        {
            var order = new Order { Currency = "USD" };
            Assert.Equal("USD", order.Currency);
        }

        [Fact]
        public void TestCurrencyWithDigits()
        {
            Assert.Throws<ArgumentException>(() => new Order { Currency = "US1" });
        }

        [Fact]
        public void TestCurrencyThatIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new Order { Currency = "US" });
        }

        [Fact]
        public void TestCurrencyThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Order { Currency = "USDE" });
        }

        [Fact]
        public void TestCurrencyInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Order { Currency = "usd" });
        }

        [Fact]
        public void TestDiscountCode()
        {
            var order = new Order { DiscountCode = "dsc" };
            Assert.Equal("dsc", order.DiscountCode);
        }

        [Fact]
        public void TestAffiliateId()
        {
            var order = new Order { AffiliateId = "af" };
            Assert.Equal("af", order.AffiliateId);
        }

        [Fact]
        public void TestSubaffiliateId()
        {
            var order = new Order { SubaffiliateId = "saf" };
            Assert.Equal("saf", order.SubaffiliateId);
        }

        [Fact]
        public void TestReferrerUri()
        {
            var uri = new Uri("http://www.mm.com/");
            var order = new Order { ReferrerUri = uri };
            Assert.Equal(uri, order.ReferrerUri);
        }

        [Fact]
        public void TestIsGift()
        {
            var order = new Order { IsGift = true };
            Assert.True(order.IsGift);
        }

        [Fact]
        public void TestHasGiftMessage()
        {
            var order = new Order { HasGiftMessage = true };
            Assert.True(order.HasGiftMessage);
        }
    }
}
