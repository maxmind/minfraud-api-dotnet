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
        public MinFraudException(string message) : base(message)
        {
        }

        public MinFraudException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected MinFraudException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}