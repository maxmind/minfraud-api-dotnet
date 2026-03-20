using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for Insights response.
    /// </summary>
    public sealed record Factors : Insights
    {
        /// <summary>
        /// This list contains objects that describe risk score reasons for a given transaction
        /// that change the risk score significantly. Risk score reasons are usually only
        /// returned for medium to high risk transactions. If there were no significant
        /// changes to the risk score due to these reasons, then this list will be empty.
        /// </summary>
        [JsonPropertyName("risk_score_reasons")]
        public IReadOnlyList<RiskScoreReason> RiskScoreReasons { get; init; }
            = new List<RiskScoreReason>().AsReadOnly();

        /// <summary>
        /// An object containing the risk factor scores for many of the
        /// individual components that are used in calculating the overall
        /// risk score.
        /// </summary>
        [JsonPropertyName("subscores")]
        [Obsolete("Replaced by RiskScoreReasons")]
        public Subscores Subscores { get; init; } = new Subscores();
    }
}
