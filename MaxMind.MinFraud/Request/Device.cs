using System;
using System.Net;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The device information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Device
    {
        private double? _sessionAge;
        private string? _sessionId;

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
            IPAddress? ipAddress = null,
            string? userAgent = null,
            string? acceptLanguage = null,
            double? sessionAge = null,
            string? sessionId = null
        )
        {
            IPAddress = ipAddress;
            UserAgent = userAgent;
            AcceptLanguage = acceptLanguage;
            SessionAge = sessionAge;
            SessionId = sessionId;
        }

        /// <summary>
        /// The IP address associated with the device used by the customer
        /// in the transaction.
        /// </summary>
        [JsonPropertyName("ip_address")]
        [JsonConverter(typeof(IPAddressConverter))]
        public IPAddress? IPAddress { get; init; }

        /// <summary>
        /// The HTTP “User-Agent” header of the browser used in the
        /// transaction.
        /// </summary>
        [JsonPropertyName("user_agent")]
        public string? UserAgent { get; init; }

        /// <summary>
        /// The HTTP “Accept-Language” header of the device used in the
        /// transaction.
        /// </summary>
        [JsonPropertyName("accept_language")]
        public string? AcceptLanguage { get; init; }

        /// <summary>
        /// The number of seconds between the creation of the user's
        /// session and the time of the transaction. Note that
        /// sessionAge is not the duration of the current visit, but
        /// the time since the start of the first visit.
        /// </summary>
        [JsonPropertyName("session_age")]
        public double? SessionAge
        {
            get => _sessionAge;
            init
            {
                if (value != null && value < 0)
                {
                    throw new ArgumentException($"{nameof(value)} must be non-negative.");
                }
                _sessionAge = value;
            }
        }

        /// <summary>
        /// A string up to 255 characters in length. This is an ID that
        /// uniquely identifies a visitor's session on the site.
        /// </summary>
        [JsonPropertyName("session_id")]
        public string? SessionId
        {
            get => _sessionId;
            init
            {
                if (value != null && value.Length > 255)
                {
                    throw new ArgumentException($"{nameof(value)} must be less than 255 characters long.");
                }
                _sessionId = value;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"IPAddress: {IPAddress}, UserAgent: {UserAgent}, AcceptLanguage: {AcceptLanguage}, SessionAge: {SessionAge}, SessionId: {SessionId}";
        }
    }
}
