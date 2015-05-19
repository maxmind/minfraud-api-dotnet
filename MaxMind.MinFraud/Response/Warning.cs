using System.Collections.Generic;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class Warning
    {
        [JsonProperty("code")]
        public string Code { get; internal set; }

        [JsonProperty("warning")]
        public string Message { get; internal set; }

        [JsonProperty("input")]
        public List<string> Input { get; internal set; } = new List<string>();

        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}, Input: {Input}";
        }
    }
}