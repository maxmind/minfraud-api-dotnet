using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the billing or shipping phone.
    /// </summary>
    public sealed class Phone
    {
        /// <summary>
        /// A two-character ISO 3166-1 country code for the country associated
        /// with the phone number.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; init; }

        /// <summary>
        /// This is <c>true</c> if the phone number is a Voice over Internet
        /// Protocol (VoIP) number allocated by a regulator. It is <c>false</c>
        /// if the phone number is not a VoIP number allocated by a regulator.
        /// The property is null if a valid phone number has not been provided
        /// or if we have data for the phone number.
        /// </summary>
        [JsonPropertyName("is_voip")]
        public bool? IsVoip { get; init; }

        /// <summary>
        /// This is <c>true</c> if the phone number's prefix is commonly
        /// associated with the postal code. It is <c>false</c> if the prefix
        /// is not associated with the postal code. It is non-<c>null</c> only
        /// when the phone number is in the US, the number prefix is in our
        /// database, and the postal code and country
        /// are provided in the request.
        /// </summary>
        [JsonPropertyName("matches_postal")]
        public bool? MatchesPostal { get; init; }

        /// <summary>
        /// The name of the original network operator associated with the
        /// phone number. This property does not reflect phone numbers that
        /// have been ported from the original operator to another, nor does
        /// it identify mobile virtual network operators.
        /// </summary>
        [JsonPropertyName("network_operator")]
        public string? NetworkOperator { get; init; }

        /// <summary>
        /// One of the following values: <c>fixed</c> or <c>mobile</c>.
        /// Additional values may be added in the future.
        /// </summary>
        [JsonPropertyName("number_type")]
        public string? NumberType { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Country: {Country}, IsVoip: {IsVoip}, MatchesPostal: {MatchesPostal}" +
                $"NetworkOperator: {NetworkOperator}, NumberType: {NumberType}";
        }
    }
}
