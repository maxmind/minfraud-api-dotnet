using Newtonsoft.Json;

namespace MaxMind.MinFraud.Util
{
    internal class WebServiceError
    {
        [JsonProperty("code")]
        internal string? Code { get; set; }

        [JsonProperty("error")]
        internal string? Error { get; set; }

        public override string ToString()
        {
            return $"Code: {Code}, Error: {Error}";
        }
    }
}