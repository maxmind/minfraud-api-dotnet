using System;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Exception
{
    internal class InvalidRequestException : MinFraudException
    {
        public InvalidRequestException(string message, string code) : base(message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("error")]
        public override string Message { get; }

        public Uri Uri { get; internal set; }
    }
}