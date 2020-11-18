#region
using MaxMind.MinFraud.Response;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
#endregion

namespace MaxMind.MinFraud.Util
{
    // See https://github.com/dotnet/runtime/issues/30083 to understand why this is
    // necessary. Hopefully we can get rid of it in a future release.
    internal class ScoreIPAddressConverter : JsonConverter<IIPAddress>
    {
        public override IIPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<ScoreIPAddress?>(
                ref reader, options) ?? new ScoreIPAddress();
        }

        public override void Write(Utf8JsonWriter writer, IIPAddress value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, typeof(ScoreIPAddress), options);
        }
    }
}