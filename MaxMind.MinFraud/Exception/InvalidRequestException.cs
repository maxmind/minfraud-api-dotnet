#region

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Newtonsoft.Json;

#endregion

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class InvalidRequestException : System.Exception
    {
        public InvalidRequestException(string message, string code, Uri uri) : base(message)
        {
            Code = code;
            Message = message;
            Uri = uri;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetString("Code");
            Uri = (Uri) info.GetValue("Uri", typeof (Uri));
        }

        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("error")]
        public override string Message { get; }

        public Uri Uri { get; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", Code);
            info.AddValue("Uri", Uri);
            base.GetObjectData(info, context);
        }
    }
}