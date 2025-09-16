using MaxMind.MinFraud.Util;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The enumerated event parties supported by the web service.
    /// </summary>
    public enum EventParty
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        [EnumMember(Value = "agent")]
        Agent,

        [EnumMember(Value = "customer")]
        Customer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// The enumerated event types supported by the web service.
    /// </summary>
    public enum EventType
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        [EnumMember(Value = "account_creation")]
        AccountCreation,

        [EnumMember(Value = "account_login")]
        AccountLogin,

        [EnumMember(Value = "credit_application")]
        CreditApplication,

        [EnumMember(Value = "email_change")]
        EmailChange,

        [EnumMember(Value = "fund_transfer")]
        FundTransfer,

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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
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
        /// <param name="party">The party submitting the transaction.</param>
        public Event(
            string? transactionId = null,
            string? shopId = null,
            DateTimeOffset? time = null,
            EventType? type = null,
            EventParty? party = null
            )
        {
            Party = party;
            TransactionId = transactionId;
            ShopId = shopId;
            Time = time;
            Type = type;
        }

        /// <summary>
        /// Constructor for backwards compatibility.
        /// </summary>
        [Obsolete("This constructor exists for binary compatibility and will be removed in next major version.")]
        public Event(
            string? transactionId,
            string? shopId,
            DateTimeOffset? time,
            EventType? type
            ) : this(transactionId, shopId, time, type, null)
        {
        }

        /// <summary>
        /// The party submitting the transaction.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<EventParty>))]
        [JsonPropertyName("party")]
        public EventParty? Party { get; init; }

        /// <summary>
        /// Your internal ID for the transaction. We can use this to locate a
        /// specific transaction in our logs, and it will also show up in email
        /// alerts and notifications from us to you.
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; init; }

        /// <summary>
        /// Your internal ID for the shop, affiliate, or merchant this order is
        /// coming from. Required for minFraud users who are resellers, payment
        /// providers, gateways and affiliate networks.
        /// </summary>
        [JsonPropertyName("shop_id")]
        public string? ShopId { get; init; }

        /// <summary>
        /// The date and time the event occurred.
        /// </summary>
        [JsonPropertyName("time")]
        public DateTimeOffset? Time { get; init; }

        /// <summary>
        /// The type of event being scored.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<EventType>))]
        [JsonPropertyName("type")]
        public EventType? Type { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Party: {Party}, TransactionId: {TransactionId}, ShopId: {ShopId}, Time: {Time}, Type: {Type}";
        }
    }
}
