using MaxMind.MinFraud.Request;
using System;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class EventTest
    {
        [Fact]
        public void TestTransactionId()
        {
            var eventReq = new Event(transactionId: "t12");
            Assert.Equal("t12", eventReq.TransactionId);
        }

        [Fact]
        public void TestShopId()
        {
            var eventReq = new Event(shopId: "s12");
            Assert.Equal("s12", eventReq.ShopId);
        }

        [Fact]
        public void TestTime()
        {
            var date = new DateTimeOffset();
            var eventReq = new Event(time: date);
            Assert.Equal(date, eventReq.Time);
        }

        [Fact]
        public void TestType()
        {
            var eventReq = new Event(type: EventType.AccountCreation);
            Assert.Equal(EventType.AccountCreation, eventReq.Type);
        }
    }
}