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
        [JsonPropertyName("country")]
        public new GeoIP2Country Country { get; init; } = new GeoIP2Country();

        /// <summary>
        /// Location object for the requested IP address.
        /// </summary>
        [JsonPropertyName("location")]
        public new GeoIP2Location Location { get; init; } = new GeoIP2Location();


        /// <summary>
        /// This property is not provided by minFraud.
        /// </summary>
        [JsonIgnore]
        [Obsolete("This is not provided in a minFraud response.")]
        public new GeoIP2.Model.MaxMind MaxMind { get; init; } = new GeoIP2.Model.MaxMind();

        /// <summary>
        /// The risk associated with the IP address. The value ranges from 0.01
        /// to 99. A higher score indicates a higher risk.
        /// </summary>
        [JsonPropertyName("risk")]
        public double? Risk { get; init; }

        /// <summary>
        /// This list contains objects identifying the reasons why the IP
        /// address received the associated risk. This will be an empty list
        /// if there are no reasons.
        /// </summary>
        [JsonPropertyName("risk_reasons")]
        public IReadOnlyList<IPRiskReason> RiskReasons { get; init; }
            = new List<IPRiskReason>().AsReadOnly();

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