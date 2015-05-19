using System;
using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class EventTest
    {
        [Test]
        public void TestTransactionId()
        {
            var eventReq = new Event(transactionId: "t12");
            Assert.AreEqual("t12", eventReq.TransactionId);
        }

        [Test]
        public void TestShopId()
        {
            var eventReq = new Event(shopId: "s12");
            Assert.AreEqual("s12", eventReq.ShopId);
        }

        [Test]
        public void TestTime()
        {
            var date = new DateTimeOffset();
            var eventReq = new Event(time: date);
            Assert.AreEqual(date, eventReq.Time);
        }

        [Test]
        public void TestType()
        {
            var eventReq = new Event(type: EventType.AccountCreation);
            Assert.AreEqual(EventType.AccountCreation, eventReq.Type);
        }
    }
}