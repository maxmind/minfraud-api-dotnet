using System.Collections.Generic;
using MaxMind.GeoIP2.Responses;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class IPLocation : InsightsResponse
    {
        [JsonProperty("country")]
        new public GeoIP2Country Country { get; internal set; } = new GeoIP2Country();

        [JsonProperty("location")]
        new public GeoIP2Location Location { get; internal set; } = new GeoIP2Location();

        internal new void SetLocales(List<string> locales)
        {
            base.SetLocales(locales);
            Country.SetLocales(locales);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Country: {Country}, Location: {Location}";
        }
    }
}