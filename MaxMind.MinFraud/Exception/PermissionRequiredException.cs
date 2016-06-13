using System;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class represents an authentication error.
    /// </summary>
    public class PermissionRequiredException : MinFraudException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PermissionRequiredException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public PermissionRequiredException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public PermissionRequiredException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}