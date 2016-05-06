﻿using MaxMind.GeoIP2.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        [JsonProperty("country")]
        public new GeoIP2Country Country { get; internal set; } = new GeoIP2Country();

        /// <summary>
        /// Location object for the requested IP address.
        /// </summary>
        [JsonProperty("location")]
        public new GeoIP2Location Location { get; internal set; } = new GeoIP2Location();

        /// <summary>
        /// The risk associated with the IP address. The value ranges from 0.01
        /// to 99. A higher score indicates a higher risk.
        /// </summary>
        [JsonProperty("risk")]
        public double? Risk { get; internal set; }

        internal new void SetLocales(IEnumerable<string> locales)
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