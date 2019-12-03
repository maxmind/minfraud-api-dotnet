using Newtonsoft.Json;
using System;
using System.Net;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The device information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Device
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ipAddress">The IP address associated with the device
        /// used by the customer in the transaction.</param>
        /// <param name="userAgent">The HTTP “User-Agent” header of the
        /// browser used in the transaction.</param>
        /// <param name="acceptLanguage">The HTTP “Accept-Language” header of
        /// the device used in the transaction.</param>
        /// <param name="sessionAge">The number of seconds between the
        /// creation of the user's session and the time of the transaction.
        /// Note that sessionAge is not the duration of the current visit, but 
        /// the time since the start of the first visit.</param>
        /// <param name="sessionId">A string up to 255 characters in length.
        /// This is an ID that uniquely identifies a visitor's session on the
        /// site.</param>
        public Device(
            IPAddress ipAddress,
            string? userAgent = null,
            string? acceptLanguage = null,
            double? sessionAge = null,
            string? sessionId = null
        )
        {
            this.IPAddress = ipAddress;
            UserAgent = userAgent;
            AcceptLanguage = acceptLanguage;

            if (sessionAge != null && sessionAge < 0)
            {
                throw new ArgumentException($"{nameof(sessionAge)} must be non-negative.");
            }
            SessionAge = sessionAge;

            if (sessionId != null && sessionId.Length > 255)
            {
                throw new ArgumentException($"{nameof(sessionId)} must be less than 255 characters long.");
            }
            SessionId = sessionId;
        }

        /// <summary>
        /// The IP address associated with the device used by the customer
        /// in the transaction.
        /// </summary>
        [JsonProperty("ip_address")]
        public IPAddress IPAddress { get; }

        /// <summary>
        /// The HTTP “User-Agent” header of the browser used in the
        /// transaction.
        /// </summary>
        [JsonProperty("user_agent")]
        public string? UserAgent { get; }

        /// <summary>
        /// The HTTP “Accept-Language” header of the device used in the
        /// transaction.
        /// </summary>
        [JsonProperty("accept_language")]
        public string? AcceptLanguage { get; }

        /// <summary>
        /// The number of seconds between the creation of the user's
        /// session and the time of the transaction. Note that 
        /// sessionAge is not the duration of the current visit, but 
        /// the time since the start of the first visit.
        /// </summary>
        [JsonProperty("session_age")]
        public double? SessionAge { get; }

        /// <summary>
        /// A string up to 255 characters in length. This is an ID that
        /// uniquely identifies a visitor's session on the site.
        /// </summary>
        [JsonProperty("session_id")]
        public string? SessionId { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"IPAddress: {IPAddress}, UserAgent: {UserAgent}, AcceptLanguage: {AcceptLanguage}";
        }
    }
}