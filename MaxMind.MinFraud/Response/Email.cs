using System;
using System.ComponentModel;
using MaxMind.MinFraud.Util;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the email address passed in the request.
    /// </summary>
    public sealed class Email
    {
        /// <summary>
        /// The date the email address was first seen by MaxMind.
        /// </summary>
        [JsonProperty("first_seen")]
        [JsonConverter(typeof(DateConverter))]
        public DateTimeOffset? FirstSeen { get; internal set; }

        /// <summary>
        /// This property is true if MaxMind believes that this email is hosted by a free
        /// email provider such as Gmail or Yahoo! Mail.
        /// </summary>
        [JsonProperty("is_free")]
        public bool? IsFree { get; internal set; }

        /// <summary>
        /// This property is true if MaxMind believes that this email is likely to be used
        ///  for fraud. Note that this is also factored into the overall risk_score in the
        /// response as well.
        /// </summary>
        [JsonProperty("is_high_risk")]
        public bool? IsHighRisk { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"IsFree: {IsFree}, IsHighRiskFree: {IsHighRisk}";
        }
    }
}