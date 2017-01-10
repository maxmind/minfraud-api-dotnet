﻿namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class represents an authentication error.
    /// </summary>
    public class AuthenticationException : MinFraudException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthenticationException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AuthenticationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public AuthenticationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}