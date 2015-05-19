using System;
using MaxMind.GeoIP2.Model;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class GeoIP2Location: Location
    {
        [JsonProperty("local_time")]
        public DateTimeOffset LocalTime { get; internal set; }

        public override string ToString()
        {
            return $"{base.ToString()}, LocalTime: {LocalTime}";
        }
    }
}