using System;
using MaxMind.GeoIP2.Model;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A subclass of the GeoIP2 Location model with minFraud-specific
    /// additions.
    /// </summary>
    public class GeoIP2Location: Location
    {
        /// <summary>
        /// The date and time of the transaction in the time
        /// zone associated with the IP address.
        /// </summary>
        [JsonProperty("local_time")]
        public DateTimeOffset LocalTime { get; internal set; }

        public override string ToString()
        {
            return $"{base.ToString()}, LocalTime: {LocalTime}";
        }
    }
}