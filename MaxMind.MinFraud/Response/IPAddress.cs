using MaxMind.GeoIP2.Responses;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for minFraud GeoIP2 Insights data.
    /// </summary>
    public sealed class IPAddress : InsightsResponse, IIPAddress
    {
        /// <summary>
        /// Country object for the requested IP address. This record represents the
        /// country where MaxMind believes the IP is located.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("country")]
        public new GeoIP2Country Country { get; internal set; } = new GeoIP2Country();

        /// <summary>
        /// Location object for the requested IP address.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("location")]
        public new GeoIP2Location Location { get; internal set; } = new GeoIP2Location();


        /// <summary>
        /// This property is not provided by minFraud.
        /// </summary>
        [JsonIgnore]
        [Obsolete("This is not provided in a minFraud response.")]
        public new GeoIP2.Model.MaxMind MaxMind { get; internal set; } = new GeoIP2.Model.MaxMind();

        /// <summary>
        /// The risk associated with the IP address. The value ranges from 0.01
        /// to 99. A higher score indicates a higher risk.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("risk")]
        public double? Risk { get; internal set; }

        internal new void SetLocales(IReadOnlyList<string> locales)
        {
            var l = new List<string>(locales);
            base.SetLocales(l);
            Country.SetLocales(l);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Country: {Country}, Location: {Location}";
        }
    }
}