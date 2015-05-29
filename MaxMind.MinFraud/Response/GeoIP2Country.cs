using System.Collections.Generic;
using MaxMind.GeoIP2.Model;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A subclass of the GeoIP2 Country model with minFraud-specific
    /// additions.
    /// </summary>
    public class GeoIP2Country: Country
    {
        /// <summary>
        /// This is <c>true</c> if the IP country is high risk.
        /// </summary>
        [JsonProperty("is_high_risk")]
        public bool? IsHighRisk { get; internal set; }

        internal void SetLocales(List<string> locales) => base.Locales = locales;

        public override string ToString()
        {
            return $"{base.ToString()}, IsHighRisk: {IsHighRisk}";
        }
    }
}