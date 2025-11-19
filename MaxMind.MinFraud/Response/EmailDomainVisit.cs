using MaxMind.MinFraud.Util;
using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the visit to the email domain.
    /// </summary>
    public sealed class EmailDomainVisit
    {
        /// <summary>
        /// This property is true if the domain automatically forwards visitors
        /// elsewhere. This property is only present when the value is true.
        /// </summary>
        [JsonPropertyName("has_redirect")]
        public bool? HasRedirect { get; init; }

        /// <summary>
        /// The date when the domain was last visited by MaxMind's automated
        /// domain inspection system.
        /// </summary>
        [JsonPropertyName("last_visited_on")]
        [JsonConverter(typeof(DateConverter))]
        public DateTimeOffset? LastVisitedOn { get; init; }

        /// <summary>
        /// The status of the domain from MaxMind's automated domain inspection.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<DomainVisitStatus>))]
        [JsonPropertyName("status")]
        public DomainVisitStatus? Status { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"HasRedirect: {HasRedirect}, LastVisitedOn: {LastVisitedOn}, Status: {Status}";
        }
    }
}
