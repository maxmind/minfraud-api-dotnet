using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class represents an authentication error.
    /// </summary>
    [Serializable]
    public class AuthenticationException : MinFraudException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AuthenticationException(string message) : base(message)
        {
        }
    }
}