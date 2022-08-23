using MaxMind.MinFraud.Request;
using MaxMind.MinFraud.Response;
using System.Threading.Tasks;

namespace MaxMind.MinFraud
{
    /// <summary>
    /// Client for querying the minFraud Score and Insights web services.
    /// </summary>
    public interface IWebServiceClient
    {
        /// <summary>
        /// Asynchronously query Factors endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud
        /// Factors response data</returns>
        Task<Factors> FactorsAsync(Transaction transaction);

        /// <summary>
        /// Asynchronously query Insights endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud
        /// Insights response data</returns>
        Task<Insights> InsightsAsync(Transaction transaction);

        /// <summary>
        /// Asynchronously query Score endpoint with transaction data
        /// </summary>
        /// <param name="transaction">Object containing the transaction data
        /// to be sent to the minFraud web service.</param>
        /// <returns>Task that produces an object modeling the minFraud Score
        /// response data</returns>
        Task<Score> ScoreAsync(Transaction transaction);

        /// <summary>
        /// Asynchronously query the minFraud Report Transaction API.
        /// </summary>
        /// <remarks>
        /// Reporting transactions to MaxMind helps us detect about 10-50% more
        /// fraud and reduce false positives for you.
        /// </remarks>
        /// <param name="report">The transaction report you would like to send.</param>
        /// <returns>The Task on which to await. The web service returns no data and
        /// this API will throw an exception if there is an error.</returns>
        Task ReportAsync(TransactionReport report);
    }
}