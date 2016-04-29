using MaxMind.MinFraud.Request;
using NUnit.Framework;
using System;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class OrderTest
    {
        [Test]
        public void TestAmount()
        {
            var order = new Order(amount: (decimal)1.1);
            Assert.AreEqual((decimal)1.1, order.Amount);
        }

        [Test]
        public void TestCurrency()
        {
            var order = new Order(currency: "USD");
            Assert.AreEqual("USD", order.Currency);
        }

        [Test]
        public void TestCurrencyWithDigits()
        {
            Assert.That(() => new Order(currency: "US1"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCurrencyThatIsTooShort()
        {
            Assert.That(() => new Order(currency: "US"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCurrencyThatIsTooLong()
        {
            Assert.That(() => new Order(currency: "USDE"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestCurrencyInWrongCase()
        {
            Assert.That(() => new Order(currency: "usd"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestDiscountCode()
        {
            var order = new Order(discountCode: "dsc");
            Assert.AreEqual("dsc", order.DiscountCode);
        }

        [Test]
        public void TestAffiliateId()
        {
            var order = new Order(affiliateId: "af");
            Assert.AreEqual("af", order.AffiliateId);
        }

        [Test]
        public void TestSubaffiliateId()
        {
            var order = new Order(subaffiliateId: "saf");
            Assert.AreEqual("saf", order.SubaffiliateId);
        }

        [Test]
        public void TestReferrerUri()
        {
            var uri = new Uri("http://www.mm.com/");
            var order = new Order(referrerUri: uri);
            Assert.AreEqual(uri, order.ReferrerUri);
        }

        [Test]
        public void TestIsGift()
        {
            var order = new Order(isGift: true);
            Assert.AreEqual(order.IsGift, true);
        }

        [Test]
        public void TestHasGiftMessage()
        {
            var order = new Order(hasGiftMessage: true);
            Assert.AreEqual(order.HasGiftMessage, true);
        }
    }
}