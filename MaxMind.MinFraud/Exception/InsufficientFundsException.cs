using System;

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class is thrown when the request fails due to insufficient funds.
    /// </summary>
    [Serializable]
    public class InsufficientFundsException : System.Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}