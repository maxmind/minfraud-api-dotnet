using MaxMind.MinFraud.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model class for Score response.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// This object contains information about the disposition set by
        /// custom rules.
        /// </summary>
        [JsonPropertyName("disposition")]
        public Disposition Disposition { get; init; } = new Disposition();

        /// <summary>
        /// The approximate US dollar value of the funds remaining on your
        /// MaxMind account.
        /// </summary>
        [JsonPropertyName("funds_remaining")]
        public decimal? FundsRemaining { get; init; }

        /// <summary>
        /// This is a UUID that identifies the minFraud request. Please use
        /// this ID in support requests to MaxMind so that we can easily
        /// identify a particular request.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid? Id { get; init; }

        /// <summary>
        /// An object containing information about the IP address's risk.
        /// </summary>
        [JsonPropertyName("ip_address")]
        [JsonConverter(typeof(ScoreIPAddressConverter))]
        public IIPAddress IPAddress { get; init; } = new ScoreIPAddress();

        /// <summary>
        /// The approximate number of queries remaining for this service
        /// before your account runs out of funds.
        /// </summary>
        [JsonPropertyName("queries_remaining")]
        public long? QueriesRemaining { get; init; }

        /// <summary>
        /// This property contains the risk score, from 0.01 to 99. A
        /// higher score indicates a higher risk of fraud.For example, a score of 20
        /// indicates a 20% chance that a transaction is fraudulent.We never return a
        /// risk score of 0, since all transactions have the possibility of being
        /// fraudulent.Likewise we never return a risk score of 100.
        /// </summary>
        [JsonPropertyName("risk_score")]
        public double? RiskScore { get; init; }

        /// <summary>
        /// This list contains objects detailing issues with the request that
        /// was sent such as invalid or unknown inputs. It is highly
        /// recommended that you check this array for issues when integrating
        /// the web service.
        /// </summary>
        [JsonPropertyName("warnings")]
        public IReadOnlyList<Warning> Warnings { get; init; } = new List<Warning>().AsReadOnly();

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var warnings = string.Join("; ", Warnings.Select(x => x.Message));
            return $"Warnings: [{warnings}], Disposition: {Disposition}, FundsRemaining: {FundsRemaining}, Id: {Id},  QueriesRemaining: {QueriesRemaining}, RiskScore: {RiskScore}";
        }
    }
}