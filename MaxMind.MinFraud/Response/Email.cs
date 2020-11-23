using MaxMind.MinFraud.Util;
using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the email address passed in the request.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// An object containing information about the email address domain.
        /// </summary>
        [JsonPropertyName("domain")]
        public EmailDomain Domain { get; init; } = new EmailDomain();

        /// <summary>
        /// The date the email address was first seen by MaxMind.
        /// </summary>
        [JsonPropertyName("first_seen")]
        [JsonConverter(typeof(DateConverter))]
        public DateTimeOffset? FirstSeen { get; init; }

        /// <summary>
        /// This property incidates whether the email is from a disposable
        /// email provider. The value will be <c>null</c> if no email address
        /// or email domain was passed as an input.
        /// </summary>
        [JsonPropertyName("is_disposable")]
        public bool? IsDisposable { get; init; }

        /// <summary>
        /// This property is true if MaxMind believes that this email is hosted by a free
        /// email provider such as Gmail or Yahoo! Mail.
        /// </summary>
        [JsonPropertyName("is_free")]
        public bool? IsFree { get; init; }

        /// <summary>
        /// This property is true if MaxMind believes that this email is likely to be used
        ///  for fraud. Note that this is also factored into the overall risk_score in the
        /// response as well.
        /// </summary>
        [JsonPropertyName("is_high_risk")]
        public bool? IsHighRisk { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Domain: {Domain}, FirstSeen: {FirstSeen}, IsDisposable: {IsDisposable}, IsFree: {IsFree}, IsHighRiskFree: {IsHighRisk}";
        }
    }
}