using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// General address response data.
    /// </summary>
    public abstract class Address
    {
        /// <summary>
        /// This property is <c>true</c> if the address is in the
        /// IP country.The property is <c>false</c> when the address is not in the IP
        /// country. If the address could not be parsed or was not provided or if the
        /// IP address could not be geolocated, the property will be <c>null</c>.
        /// </summary>
        [JsonProperty("is_in_ip_country")]
        public bool? IsInIPCountry { get; internal set; }

        /// <summary>
        /// This property is <c>true</c> if the postal code
        /// provided with the address is in the city for the address.The property is
        /// <c>false</c> when the postal code is not in the city. If the address was
        /// not provided or could not be parsed, the property will be <c>null</c>.
        /// </summary>
        [JsonProperty("is_postal_in_city")]
        public bool? IsPostalInCity { get; internal set; }

        /// <summary>
        /// The latitude associated with the address.
        /// </summary>
        [JsonProperty("latitude")]
        public double? Latitude { get; internal set; }

        /// <summary>
        /// The longitude associated with the address.
        /// </summary>
        [JsonProperty("longitude")]
        public double? Longitude { get; internal set; }

        /// <summary>
        /// The distance in kilometers from the address to the IP location.
          /// </summary>
        [JsonProperty("distance_to_ip_location")]
        public int? DistanceToIPLocation { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"IsInIPCountry: {IsInIPCountry}, IsPostalInCity: {IsPostalInCity}, Latitude: {Latitude}, Longitude: {Longitude}, DistanceToIPLocation: {DistanceToIPLocation}";
        }
    }
}
