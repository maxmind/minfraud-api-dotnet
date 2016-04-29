using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// The IP addresses risk.
    /// </summary>
    public class ScoreIPAddress : IIPAddress
    {
        /// <summary>
        /// The risk associated with the IP address. The value ranges from 0.01
        /// to 99. A higher score indicates a higher risk.
        /// </summary>
        [JsonProperty("risk")]
        public double? Risk { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Risk: {Risk}";
        }
    }
}