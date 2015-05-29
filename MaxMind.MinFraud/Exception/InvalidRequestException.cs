#region

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Newtonsoft.Json;

#endregion

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    /// This class is thrown when the web service rejected the request. Check
    /// the value of <code>Code</code> for the reason code.
    /// </summary>
    [Serializable]
    public class InvalidRequestException : MinFraudException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The message from the web service.</param>
        /// <param name="code">The machine-readable error code.</param>
        /// <param name="uri">The URI that was queried.</param>
        public InvalidRequestException(string message, string code, Uri uri) : base(message)
        {
            Code = code;
            this.Uri = uri;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetString("Code");
            this.Uri = (Uri) info.GetValue("Uri", typeof (Uri));
        }

        /// <summary>
        /// The reason code for why the web service rejected the request
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The URI that was used for the request.
        /// </summary>
        public Uri Uri { get; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", Code);
            info.AddValue("Uri", this.Uri);
            base.GetObjectData(info, context);
        }
    }
}
