using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud
{
    // See https://github.com/dotnet/runtime/issues/31081. We could also switch
    // to a snakecase naming policy of that is added:
    // https://github.com/dotnet/runtime/issues/782
    internal class EnumMemberValueConverter<T> : JsonConverter<T> where T : Enum
    {
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var memInfo = typeof(T).GetMember(value.ToString());
            var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null)
            {
                writer.WriteStringValue(attr.Value);
                return;
            }

            writer.WriteNullValue();
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Read is not implemented");
        }
    }
}