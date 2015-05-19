using System;

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class InsufficientFundsException : System.Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}