using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains information about the device that MaxMind
    /// believes is associated with the IP address passed in the request.
    /// In order to receive device output from minFraud Insights or
    /// minFraud Factors, you must be using the
    /// <see href="https://dev.maxmind.com/minfraud/track-devices?lang=en">Device
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
        [JsonPropertyName("confidence")]
        public double? Confidence { get; init; }

        /// <summary>
        /// A UUID that MaxMind uses for the device associated with this IP
        /// address.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid? Id { get; init; }

        /// <summary>
        /// The date and time of the last sighting of the device.
        /// </summary>
        [JsonPropertyName("last_seen")]
        public DateTimeOffset? LastSeen { get; init; }

        /// <summary>
        /// The local date and time of the transaction in the time zone of
        /// the device. This is determined by using the UTC offset associated
        /// with the device. This is an RFC 3339 date-time
        /// </summary>
        [JsonPropertyName("local_time")]
        public DateTimeOffset? LocalTime { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Confidence: {Confidence}, Id: {Id}, LastSeen: {LastSeen}, LocalTime: {LocalTime}";
        }
    }
}
