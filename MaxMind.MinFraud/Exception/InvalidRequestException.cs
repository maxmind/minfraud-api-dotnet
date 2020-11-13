#region

using System;

#endregion

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class is thrown when the web service rejected the request. Check
    /// the value of <c>Code</c> for the reason code.
    /// </summary>
    public class InvalidRequestException : MinFraudException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InvalidRequestException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message from the web service.</param>
        public InvalidRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message from the web service.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public InvalidRequestException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message from the web service.</param>
        /// <param name="code">The machine-readable error code.</param>
        /// <param name="uri">The URI that was queried.</param>
        public InvalidRequestException(string message, string code, Uri? uri) : base(message)
        {
            Code = code;
            this.Uri = uri;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message from the web service.</param>
        /// <param name="code">The machine-readable error code.</param>
        /// <param name="uri">The URI that was queried.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public InvalidRequestException(string message, string code, Uri? uri, System.Exception innerException) : base(message, innerException)
        {
            Code = code;
            this.Uri = uri;
        }

        /// <summary>
        /// The <a href="https://dev.maxmind.com/minfraud/#Errors">
        /// reason code</a> for why the web service rejected the request.
        /// </summary>
        public string? Code { get; }

        /// <summary>
        /// The URI that was used for the request.
        /// </summary>
        public Uri? Uri { get; }
    }
}