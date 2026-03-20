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
            var eventReq = new Event { TransactionId = "t12" };
            Assert.Equal("t12", eventReq.TransactionId);
        }

        [Fact]
        public void TestShopId()
        {
            var eventReq = new Event { ShopId = "s12" };
            Assert.Equal("s12", eventReq.ShopId);
        }

        [Fact]
        public void TestTime()
        {
            var date = new DateTimeOffset();
            var eventReq = new Event { Time = date };
            Assert.Equal(date, eventReq.Time);
        }

        [Fact]
        public void TestType()
        {
            var eventReq = new Event { Type = EventType.AccountCreation };
            Assert.Equal(EventType.AccountCreation, eventReq.Type);
        }

        [Fact]
        public void TestCreditApplicationType()
        {
            var eventReq = new Event { Type = EventType.CreditApplication };
            Assert.Equal(EventType.CreditApplication, eventReq.Type);
        }

        [Fact]
        public void TestFundTransferType()
        {
            var eventReq = new Event { Type = EventType.FundTransfer };
            Assert.Equal(EventType.FundTransfer, eventReq.Type);
        }

        [Fact]
        public void TestAgentParty()
        {
            var eventReq = new Event { Party = EventParty.Agent };
            Assert.Equal(EventParty.Agent, eventReq.Party);
        }

        [Fact]
        public void TestCustomerParty()
        {
            var eventReq = new Event { Party = EventParty.Customer };
            Assert.Equal(EventParty.Customer, eventReq.Party);
        }

        [Fact]
        public void TestSerialization()
        {
            var eventReq = new Event
            {
                TransactionId = "txn123",
                ShopId = "shop123",
                Time = new DateTimeOffset(2020, 7, 12,
                                         15, 30, 0, 0,
                                         new TimeSpan(2, 0, 0)),
                Type = EventType.AccountCreation,
                Party = EventParty.Agent
            };

            var json = JsonSerializer.Serialize(eventReq);
            var comparer = new JsonElementComparer();
            Assert.True(
                comparer.JsonEquals(
                    JsonDocument.Parse(
                        """
                        {
                            "party": "agent",
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
