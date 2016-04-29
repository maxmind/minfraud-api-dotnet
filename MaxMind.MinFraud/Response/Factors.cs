using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for Insights response.
    /// </summary>
    public class Factors : Insights
    {
        /// <summary>
        /// An object containing GeoIP2 and minFraud Insights information about
        /// the IP address.
        /// </summary>
        [JsonProperty("subscores")]
        public Subscores Subscores { get; internal set; } = new Subscores();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, Subscores: {Subscores}";
        }
    }
}