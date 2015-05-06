using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class AuthenticationException : MinFraudException
    {
        public AuthenticationException(string message) : base(message)
        {
        }
    }
}