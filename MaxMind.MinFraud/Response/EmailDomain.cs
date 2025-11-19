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
        /// The classification of the email domain.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<DomainClassification>))]
        [JsonPropertyName("classification")]
        public DomainClassification? Classification { get; init; }

        /// <summary>
        /// The date the email address domain was first seen by MaxMind.
        /// </summary>
        [JsonPropertyName("first_seen")]
        [JsonConverter(typeof(DateConverter))]
        public DateTimeOffset? FirstSeen { get; init; }

        /// <summary>
        /// A risk score for the email domain, ranging from 0.01 to 99. Higher scores
        /// indicate a greater risk associated with the domain.
        /// </summary>
        [JsonPropertyName("risk")]
        public double? Risk { get; init; }

        /// <summary>
        /// The activity indicator for the email domain across the minFraud network,
        /// expressed as sightings per million, rounded to 2 significant figures.
        /// This value ranges from 0.001 to 1,000,000.
        /// </summary>
        [JsonPropertyName("volume")]
        public double? Volume { get; init; }

        /// <summary>
        /// Information about the visit to the email domain.
        /// </summary>
        [JsonPropertyName("visit")]
        public EmailDomainVisit? Visit { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Classification: {Classification}, FirstSeen: {FirstSeen}, Risk: {Risk}, Volume: {Volume}, Visit: {Visit}";
        }
    }
}