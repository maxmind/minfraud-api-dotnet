using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The enumerated event types supported by the web service.
    /// </summary>
    public enum EventType
    {
#pragma warning disable CS1591

        [EnumMember(Value = "account_creation")]
        AccountCreation,

        [EnumMember(Value = "account_login")]
        AccountLogin,

        [EnumMember(Value = "email_change")]
        EmailChange,

        [EnumMember(Value = "password_reset")]
        PasswordReset,

        [EnumMember(Value = "payout_change")]
        PayoutChange,

        [EnumMember(Value = "purchase")]
        Purchase,

        [EnumMember(Value = "recurring_purchase")]
        RecurringPurchase,

        [EnumMember(Value = "referral")]
        Referral,

        [EnumMember(Value = "survey")]
        Survey

#pragma warning restore
    }

    /// <summary>
    /// Event information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Event
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="transactionId">Your internal ID for the transaction.
        /// We can use this to locate a specific transaction in our logs, and
        /// it will also show up in email alerts and notifications from us to
        /// you.</param>
        /// <param name="shopId">Your internal ID for the shop, affiliate, or
        /// merchant this order is coming from. Required for minFraud users
        /// who are resellers, payment providers, gateways and affiliate
        /// networks.</param>
        /// <param name="time">The date and time the event occurred. If this
        /// field is not in the request, the current time will be used.</param>
        /// <param name="type">The type of event being scored.</param>
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
            this.Type = type;
        }

        /// <summary>
        /// Your internal ID for the transaction. We can use this to locate a
        /// specific transaction in our logs, and it will also show up in email
        /// alerts and notifications from us to you.
        /// </summary>
        [JsonProperty("transaction_id")]
        public string TransactionId { get; }

        /// <summary>
        /// Your internal ID for the shop, affiliate, or merchant this order is
        /// coming from. Required for minFraud users who are resellers, payment
        /// providers, gateways and affiliate networks.
        /// </summary>
        [JsonProperty("shop_id")]
        public string ShopId { get; }

        /// <summary>
        /// The date and time the event occurred.
        /// </summary>
        [JsonProperty("time")]
        public DateTimeOffset? Time { get; }

        /// <summary>
        /// The type of event being scored.
        /// </summary>
        [JsonProperty("type")]
        public EventType? Type { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"TransactionId: {TransactionId}, ShopId: {ShopId}, Time: {Time}, Type: {Type}";
        }
    }
}
