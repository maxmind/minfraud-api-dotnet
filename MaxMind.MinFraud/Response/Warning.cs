using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A warning returned by the web service.
    /// </summary>
    public sealed class Warning
    {
        [JsonProperty("input")]
        internal List<string> _input = new List<string>();

        /// <summary>
        /// This value is a machine-readable code identifying the
        /// warning. See the <a href="http://dev.maxmind.com/minfraud-score-and-insights-api-documentation/#Warning_Object">
        /// web service documentation</a> for the current list of of warning
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
        public ReadOnlyCollection<string> Input => new ReadOnlyCollection<string>(_input);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}, Input: {Input}";
        }
    }
}