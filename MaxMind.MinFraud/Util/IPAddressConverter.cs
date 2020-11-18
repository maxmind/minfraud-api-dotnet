using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud
{
    internal class IPAddressConverter : JsonConverter<IPAddress?>
    {
        public override void Write(Utf8JsonWriter writer, IPAddress? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }

        public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
            {
                return null;
            }

            return IPAddress.Parse(value);
        }
    }
}