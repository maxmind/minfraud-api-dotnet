using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// General address response data.
    /// </summary>
    public abstract class Address
    {
        /// <summary>
        /// This property is <c>true</c> if the address is in the
        /// IP country. The property is <c>false</c> when the address is not in the IP
        /// country. If the address could not be parsed or was not provided or if the
        /// IP address could not be geolocated, the property will be <c>null</c>.
        /// </summary>
        [JsonPropertyName("is_in_ip_country")]
        public bool? IsInIPCountry { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the postal code
        /// provided with the address is in the city for the address. The property is
        /// <c>false</c> when the postal code is not in the city. If the address was
        /// not provided or could not be parsed, the property will be <c>null</c>.
        /// </summary>
        [JsonPropertyName("is_postal_in_city")]
        public bool? IsPostalInCity { get; init; }

        /// <summary>
        /// The latitude associated with the address.
        /// </summary>
        [JsonPropertyName("latitude")]
        public double? Latitude { get; init; }

        /// <summary>
        /// The longitude associated with the address.
        /// </summary>
        [JsonPropertyName("longitude")]
        public double? Longitude { get; init; }

        /// <summary>
        /// The distance in kilometers from the address to the IP location.
        /// </summary>
        [JsonPropertyName("distance_to_ip_location")]
        public int? DistanceToIPLocation { get; init; }

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
