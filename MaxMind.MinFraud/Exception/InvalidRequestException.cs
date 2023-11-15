#region

using System;
using System.Runtime.Serialization;

#endregion

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class is thrown when the web service rejected the request. Check
    /// the value of <c>Code</c> for the reason code.
    /// </summary>
    [Serializable]
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
            Uri = uri;
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
            Uri = uri;
        }

        /// <summary>
        ///     Constructor for deserialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
#if NET8_0_OR_GREATER
        [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
        protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Code = info.GetString("MaxMind.MinFraud.Exception.InvalidRequestException.Code")
                ?? throw new SerializationException("Unexpected null Code value");
            Uri = (Uri)(info.GetValue("MaxMind.MinFraud.Exception.InvalidRequestException.Uri", typeof(Uri))
                ?? throw new SerializationException("Unexpected null Uri value"));
        }

        /// <summary>
        ///     Method to serialize data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
#if NET8_0_OR_GREATER
        [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("MaxMind.MinFraud.Exception.InvalidRequestException.Code", Code);
            info.AddValue("MaxMind.MinFraud.Exception.InvalidRequestException.Uri", Uri, typeof(Uri));
        }

        /// <summary>
        /// The <a href="https://dev.maxmind.com/minfraud/api-documentation/responses?lang=en#errors">
        /// reason code</a> for why the web service rejected the request.
        /// </summary>
        public string? Code { get; init; }

        /// <summary>
        /// The URI that was used for the request.
        /// </summary>
        public Uri? Uri { get; init; }
    }
}
