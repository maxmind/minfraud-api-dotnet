using System;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class is thrown when the request fails due to insufficient funds.
    /// </summary>
    public class InsufficientFundsException : System.Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public InsufficientFundsException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InsufficientFundsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public InsufficientFundsException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}