using System;

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class InvalidInputException : System.Exception
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}