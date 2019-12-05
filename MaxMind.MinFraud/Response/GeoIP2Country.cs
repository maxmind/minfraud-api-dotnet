using MaxMind.GeoIP2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A subclass of the GeoIP2 Country model with minFraud-specific
    /// additions.
    /// </summary>
    public sealed class GeoIP2Country : Country
    {
        /// <summary>
        /// This is <c>true</c> if the IP country is high risk.
        /// </summary>
        [JsonProperty("is_high_risk")]
        [Obsolete("Deprecated effective August 29, 2019.")]
        public bool? IsHighRisk { get; internal set; }

        internal void SetLocales(IEnumerable<string> locales) => Locales = locales;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return $"{base.ToString()}, IsHighRisk: {IsHighRisk}";
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}