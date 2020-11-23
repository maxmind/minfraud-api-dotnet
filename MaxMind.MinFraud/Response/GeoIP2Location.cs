using MaxMind.GeoIP2.Model;
using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A subclass of the GeoIP2 Location model with minFraud-specific
    /// additions.
    /// </summary>
    public sealed class GeoIP2Location : Location
    {
        /// <summary>
        /// The date and time of the transaction in the time
        /// zone associated with the IP address.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("local_time")]
        public DateTimeOffset? LocalTime { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, LocalTime: {LocalTime}";
        }
    }
}