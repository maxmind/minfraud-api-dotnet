using System;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class represents a non-specific error with data returned by
    /// the web service.
    /// </summary>
    public class MinFraudException : System.Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MinFraudException()
        {
        }

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
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public MinFraudException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}