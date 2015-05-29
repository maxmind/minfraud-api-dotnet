using System.Collections.Generic;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class Warning
    {
        [JsonProperty("input")]
        internal List<string> _input = new List<string>();

        /// <summary>
        /// This value is a machine-readable code identifying the
        /// warning. See the <see href="http://dev.maxmind.com/minfraud-score-and-insights-api-documentation/#Warning_Object">
        /// web service documentation</see> for the current list of of warning
        ///  codes.
        ///</summary>
        [JsonProperty("code")]
        public string Code { get; internal set; }

        /// <summary>
        /// This property provides a human-readable explanation of the
        /// warning. The description may change at any time and should not be
        /// matched against.
        /// </summary>
        [JsonProperty("warning")]
        public string Message { get; internal set; }

        /// <summary>
        /// This is a list of keys representing the path to the input that
        /// the warning is associated with. For instance, if the warning was
        /// about the billing city, the list would be 
        /// {"billing", "city"}. The key is used for an object and the index
        /// number for an array.
        /// </summary>
        [JsonIgnore]
        public List<string> Input => new List<string>(_input);

        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}, Input: {Input}";
        }
    }
}