using MaxMind.MinFraud.Util;
using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the email address domain passed
    /// in the request.
    /// </summary>
    public sealed class EmailDomain
    {
        /// <summary>
        /// The date the email address domain was first seen by MaxMind.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("first_seen")]
        [JsonConverter(typeof(DateConverter))]
        public DateTimeOffset? FirstSeen { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"FirstSeen: {FirstSeen}";
        }
    }
}