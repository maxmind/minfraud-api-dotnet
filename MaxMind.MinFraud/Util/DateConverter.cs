#region
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
#endregion

namespace MaxMind.MinFraud.Util
{
    internal class DateConverter : JsonConverter<DateTimeOffset?>
    {
        private const string format = "yyyy-MM-dd";

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var v = reader.GetString();
            if (v == null)
            {
                return null;
            }
            return DateTimeOffset.ParseExact(
                v,
                format,
                DateTimeFormatInfo.InvariantInfo,
                DateTimeStyles.None);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(format));
        }
    }
}