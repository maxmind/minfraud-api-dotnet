using MaxMind.Db;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Util
{
    /// <summary>
    /// A JsonConverter for MaxMind.Db.Network.
    /// </summary>
    public class NetworkConverter : JsonConverter<Network?>
    {
        /// <summary>
        /// Reads a JSON value.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, Network? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }

        /// <summary>
        /// Writes a JSON value.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override Network? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
            {
                return null;
            }
            // It'd probably be nice if we added an appropriate constructor
            // to Network.
            var parts = value.Split('/');
            if (parts.Length != 2 || !int.TryParse(parts[1], out var prefixLength))
            {
                throw new JsonException("Network not in CIDR format: " + value);
            }

            return new Network(IPAddress.Parse(parts[0]), prefixLength);
        }
    }
}