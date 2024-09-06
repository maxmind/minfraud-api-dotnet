using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for a risk score multiplier and reasons for that multiplier.
    /// </summary>
    public sealed class RiskScoreReason
    {
        /// <summary>
        /// The factor by which the risk score is increased (if the value is greater than 1)
        /// or decreased (if the value is less than 1) for given risk reason(s).
        /// Multipliers greater than 1.5 and less than 0.66 are considered
        /// significant and lead to risk reason(s) being present.
        /// </summary>
        [JsonPropertyName("multiplier")]
        public double? Multiplier { get; init; }

        /// <summary>
        /// This list contains objects that describe one of the reasons for the multiplier.
        /// </summary>
        [JsonPropertyName("reasons")]
        public IReadOnlyList<MultiplierReason> Reasons { get; init; }
            = new List<MultiplierReason>().AsReadOnly();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Multiplier: {Multiplier}, Reasons: {Reasons}";
        }
    }
}
