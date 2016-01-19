using Newtonsoft.Json;
using System;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the device that MaxMind
    /// believes is associated with the IP address passed in the request.
    /// </summary>
    public sealed class Device
    {
        /// <summary>
        /// A UUID that MaxMind uses for the device associated with this IP
        /// address. Note that many devices cannot be uniquely identified
        /// because they are too common (for example, all iPhones of a given
        /// model and OS release). In these cases, the minFraud service will
        /// simply not return a UUID for that device.
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return
                $"Id: {Id}";
        }
    }
}