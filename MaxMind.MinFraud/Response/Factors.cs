using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for Insights response.
    /// </summary>
    public sealed class Factors : Insights
    {
        /// <summary>
        /// An object containing the risk factor scores for many of the
        /// individual components that are used in calculating the overall
        /// risk score.
        /// </summary>
        [JsonPropertyName("subscores")]
        public Subscores Subscores { get; init; } = new Subscores();

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
