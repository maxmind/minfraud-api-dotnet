using MaxMind.MinFraud.Request;
using System;
using System.Text.Json;
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

        [Fact]
        public void TestSerialization()
        {
            var eventReq = new Event(
                transactionId: "txn123",
                shopId: "shop123",
                time: new DateTimeOffset(2020, 7, 12,
                                         15, 30, 0, 0,
                                         new TimeSpan(2, 0, 0)),
                type: EventType.AccountCreation
            );

            var json = JsonSerializer.Serialize(eventReq);
            var comparer = new JsonElementComparer();
            Assert.True(
                comparer.JsonEquals(
                    JsonDocument.Parse(
                        """
                        {
                            "transaction_id": "txn123",
                            "shop_id": "shop123",
                            "time": "2020-07-12T15:30:00+02:00",
                            "type": "account_creation"
                        }
                        """),
                    JsonDocument.Parse(json)
                ),
                json
            );
        }
    }
}
