using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Exception
{
    [Serializable]
    public class InvalidRequestException : System.Exception
    {
        public InvalidRequestException(string message, string code) : base(message)
        {
            Code = code;
            Message = message;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Code = info.GetString("Code");
            this.Uri = (Uri) info.GetValue("Uri", typeof (Uri));
        }


        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("error")]
        public override string Message { get; }

        public Uri Uri { get; internal set; }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", this.Code);
            info.AddValue("Uri", this.Uri);
            base.GetObjectData(info, context);
        }
    }
}