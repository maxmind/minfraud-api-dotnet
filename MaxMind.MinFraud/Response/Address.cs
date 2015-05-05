using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public abstract class Address
    {
        [JsonProperty("is_in_ip_country")]
        public bool? IsInIpCountry { get; internal set; }

        [JsonProperty("is_postal_in_city")]
        public bool? IsPostalInCity { get; internal set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; internal set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; internal set; }

        [JsonProperty("distance_to_ip_location")]
        public int? DistanceToIpLocation { get; internal set; }

        public override string ToString()
        {
            return
                $"IsInIpCountry: {IsInIpCountry}, IsPostalInCity: {IsPostalInCity}, Latitude: {Latitude}, Longitude: {Longitude}, DistanceToIpLocation: {DistanceToIpLocation}";
        }
    }
}