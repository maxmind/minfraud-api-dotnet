using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class Issuer
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("matches_provided_name")]
        public bool? MatchesProvidedName { get; internal set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; internal set; }

        [JsonProperty("matches_provided_phone_number")]
        public bool? MatchesProvidedPhoneNumber { get; internal set; }

        public override string ToString()
        {
            return
                $"Name: {Name}, MatchesProvidedName: {MatchesProvidedName}, PhoneNumber: {PhoneNumber}, MatchesProvidedPhoneNumber: {MatchesProvidedPhoneNumber}";
        }
    }
}