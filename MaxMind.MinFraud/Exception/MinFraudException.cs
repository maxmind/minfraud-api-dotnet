using System;

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class MinFraudException : System.Exception
    {
        public MinFraudException(string message) : base(message)
        {
        }

        public MinFraudException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}