using System;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class represents a non-specific error with data returned by
    /// the web service.
    /// </summary>
    [Serializable]
    public class MinFraudException : System.Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public MinFraudException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">The previous exception.</param>
        public MinFraudException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        /// <param name="info">The SerializationInfo with data.</param>
        /// <param name="context">The source for this deserialization.</param>
        protected MinFraudException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}