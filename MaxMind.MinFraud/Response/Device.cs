using Newtonsoft.Json;
using System;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the device that MaxMind
    /// believes is associated with the IP address passed in the request.
    /// In order to receive device output from minFraud Insights or
    /// minFraud Factors, you must be using the
    /// <see href="https://dev.maxmind.com/minfraud/device/">Device
    /// Tracking Add-on</see>.
    /// </summary>
    public sealed class Device
    {
        /// <summary>
        /// A number representing the confidence that the <c>DeviceId</c>
        /// refers to a unique device as opposed to a cluster of similar
        /// devices. A confidence of 0.01 indicates very low confidence that
        /// the device is unique, whereas 99 indicates very high confidence.
        /// </summary>
        [JsonProperty("confidence")]
        public double? Confidence { get; internal set; }

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
        /// The date and time of the last sighting of the device.
        /// </summary>
        [JsonProperty("last_seen")]
        public DateTimeOffset? LastSeen { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Confidence: {Confidence}, Id: {Id}, LastSeen: {LastSeen}";
        }
    }
}