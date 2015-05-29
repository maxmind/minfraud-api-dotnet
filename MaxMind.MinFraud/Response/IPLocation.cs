using System.Collections.Generic;
using MaxMind.GeoIP2.Responses;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for minFraud GeoIP2 Insights data.
    /// </summary>
    public class IPLocation : InsightsResponse
    {
        /// <summary>
        /// Country object for the requested IP address. This record represents the
        /// country where MaxMind believes the IP is located.
        /// </summary>
        [JsonProperty("country")]
        new public GeoIP2Country Country { get; internal set; } = new GeoIP2Country();

        /// <summary>
        /// Location object for the requested IP address.
        /// </summary>
        [JsonProperty("location")]
        new public GeoIP2Location Location { get; internal set; } = new GeoIP2Location();

        internal new void SetLocales(List<string> locales)
        {
            base.SetLocales(locales);
            Country.SetLocales(locales);
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