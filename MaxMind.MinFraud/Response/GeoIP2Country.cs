using System.Collections.Generic;
using MaxMind.GeoIP2.Model;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class GeoIP2Country: Country
    {
        [JsonProperty("is_high_risk")]
        public bool? IsHighRisk { get; internal set; }

        internal void SetLocales(List<string> locales) => base.Locales = locales;

        public override string ToString()
        {
            return $"{base.ToString()}, IsHighRisk: {IsHighRisk}";
        }
    }
}