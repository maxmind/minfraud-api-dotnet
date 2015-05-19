using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public enum EventType
    {
        [EnumMember(Value = "account_creation")] AccountCreation,
        [EnumMember(Value = "account_login")] AccountLogin,
        [EnumMember(Value = "purchase")] Purchase,
        [EnumMember(Value = "recurring_purchase")] RecurringPurchase,
        [EnumMember(Value = "referral")] Referral,
        [EnumMember(Value = "survey")] Survey
    }

    public class Event
    {
        public Event(
            string transactionId = null,
            string shopId = null,
            DateTimeOffset? time = null,
            EventType? type = null
            )
        {
            TransactionId = transactionId;
            ShopId = shopId;
            Time = time;
            Type = type;
        }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; }

        [JsonProperty("shop_id")]
        public string ShopId { get; }

        // JSON.NET should format this as an ISO-8601 date
        [JsonProperty("time")]
        public DateTimeOffset? Time { get; }

        [JsonProperty("type")]
        public EventType? Type { get; }

        public override string ToString()
        {
            return $"TransactionId: {TransactionId}, ShopId: {ShopId}, Time: {Time}, Type: {Type}";
        }
    }
}