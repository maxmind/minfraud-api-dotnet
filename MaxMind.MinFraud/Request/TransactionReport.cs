using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// Enumerated transaction report tags supported by the web service.
    /// </summary>
    public enum TransactionReportTag
    {
        /// <summary>
        /// The transaction was not fraudulent.
        /// </summary>
        [EnumMember(Value = "not_fraud")]
        NotFraud,

        /// <summary>
        /// You suspect the transaction is fraudulent but you have not
        /// received a chargeback.
        /// </summary>
        [EnumMember(Value = "suspected_fraud")]
        SuspectedFraud,

        /// <summary>
        /// The transaction is spam or abuse but you have not received a
        /// chargeback.
        /// </summary>
        [EnumMember(Value = "spam_or_abuse")]
        SpamOrAbuse,

        /// <summary>
        /// There was a chargeback on this transaction.
        /// </summary>
        [EnumMember(Value = "chargeback")]
        Chargeback,
    }

    /// <summary>
    /// The transaction information for a report you would like to file with
    /// MaxMind.
    /// </summary>
    public sealed class TransactionReport
    {
        private string? _maxmindId;

        /// <summary>
        /// Constructor with validation.
        /// </summary>
        /// <param name="ipAddress">The IP address reported to MaxMind for the
        ///     transaction. This field is not required if you provide at least
        ///     one of the transaction's <c>minfraudId</c>, <c>maxmindId</c>,
        ///     or <c>transactionId></c>. You are encouraged to provide it,
        ///     if possible.</param>
        /// <param name="tag">The <c>TransactionReportTag</c> indicating the
        ///     type of report being made.</param>
        /// <param name="chargebackCode">A string which is provided by your
        ///     payment processor indicating <a href="https://en.wikipedia.org/wiki/Chargeback#Reason_codes">
        ///     the reason for the chargeback</a>.</param>
        /// <param name="maxmindId">A unique eight character string identifying
        ///     a minFraud Standard or Premium request. These IDs are returned
        ///     in the <c>maxmindID</c> field of a response for a successful
        ///     minFraud request. This field is not required if you provide at
        ///     least one of the transaction's <c>ipAddress</c>,
        ///     <c>minfraudId</c>, or <c>transactionId></c>. You are encouraged
        ///     to provide it, if possible.</param>
        /// <param name="minfraudId">A UUID that identifies a minFraud Score,
        ///     minFraud Insights, or minFraud Factors request. This ID is
        ///     returned at <c>/id</c> in the response. This field is not
        ///     required if you provide at least one of the transaction's
        ///     <c>ipAddress</c>, <c>maxmindId</c>, or <c>transactionId></c>.
        ///     You are encouraged to provide it, if possible.</param>
        /// <param name="notes">Your notes on the fraud tag associated with the
        ///     transaction. We manually review many reported transactions to
        ///     improve our scoring for you so any additional details to help
        ///     us understand context are helpful.</param>
        /// <param name="transactionId">The transaction ID you originally passed
        ///     to minFraud. This field is not required if you provide at least
        ///     one of the transaction's <c>ipAddress</c>, <c>maxmindId</c>,
        ///     or <c>minfraudId></c>. You are encouraged to provide it, if
        ///     possible.</param>
        public TransactionReport(
            IPAddress? ipAddress,
            TransactionReportTag tag,
            string? chargebackCode = null,
            string? maxmindId = null,
            Guid? minfraudId = null,
            string? notes = null,
            string? transactionId = null
        )
        {
            if (ipAddress == null && minfraudId == null && string.IsNullOrEmpty(maxmindId)
                && string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException(
                    "The user must pass at least one of the following: " +
                    "ipAddress, minfraudId, maxmindId, transactionId."
                );
            }

            IPAddress = ipAddress;
            Tag = tag;
            ChargebackCode = chargebackCode;
            MaxMindId = maxmindId;
            MinFraudId = minfraudId;
            Notes = notes;
            TransactionId = transactionId;
        }

        /// <summary>
        /// The IP address reported to MaxMind for the transaction.
        /// </summary>
        [JsonPropertyName("ip_address")]
        [JsonConverter(typeof(IPAddressConverter))]
        public IPAddress? IPAddress { get; init; }

        /// <summary>
        /// The <c>TransactionReportTag</c> indicating the type of report
        /// being made.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<TransactionReportTag>))]
        [JsonPropertyName("tag")]
        public TransactionReportTag Tag { get; init; }

        /// <summary>
        /// A string which is provided by your payment processor indicating
        /// <a href="https://en.wikipedia.org/wiki/Chargeback#Reason_codes">
        /// the reason for the chargeback</a>.
        /// </summary>
        [JsonPropertyName("chargeback_code")]
        public string? ChargebackCode { get; init; }

        /// <summary>
        /// A unique eight character string identifying a minFraud Standard or
        /// Premium request. These IDs are returned in the <c>maxmindID</c>
        /// field of a response for a successful minFraud request. This field
        /// is not required, but you are encouraged to provide it, if possible.
        /// </summary>
        [JsonPropertyName("maxmind_id")]
        public string? MaxMindId
        {
            get => _maxmindId;
            init
            {
                if (value != null && value.Length != 8)
                {
                    throw new ArgumentException($"{nameof(value)} must be an eight character string.");
                }
                _maxmindId = value;
            }
        }

        /// <summary>
        /// A UUID that identifies a minFraud Score, minFraud Insights, or
        /// minFraud Factors request. This ID is returned at <c>/id</c> in the
        /// response. This field is not required, but you are encouraged to
        /// provide it if the request was made to one of these services.
        /// </summary>
        [JsonPropertyName("minfraud_id")]
        public Guid? MinFraudId { get; init; }

        /// <summary>
        /// Your notes on the fraud tag associated with the transaction. We
        /// manually review many reported transactions to improve our scoring
        /// for you so any additional details to help us understand context
        /// are helpful.
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; init; }

        /// <summary>
        /// The transaction ID you originally passed to minFraud. This field
        /// is not required, but you are encouraged to provide it or the
        /// transaction's <c>maxmindId</c> or <c>minfraudId</c>.
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"IPAddress: {IPAddress}, Tag: {Tag}, ChargebackCode: {ChargebackCode}, MaxMindId: {MaxMindId}, "
                + $"MinFraudId: {MinFraudId}, Notes: {Notes}, TransactionId: {TransactionId}";
        }
    }
}