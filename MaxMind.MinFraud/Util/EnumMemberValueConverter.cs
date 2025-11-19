using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Util
{
    // See https://github.com/dotnet/runtime/issues/31081. We could also switch
    // to a snakecase naming policy of that is added:
    // https://github.com/dotnet/runtime/issues/782
    //
    // This converter handles both nullable (T?) and non-nullable (T) enum properties.
    // Unknown enum values from the API are gracefully handled by returning null,
    // ensuring forward compatibility when new enum values are added.
    internal class EnumMemberValueConverter<T> : JsonConverter<object> where T : struct, Enum
    {
        private static readonly Dictionary<string, T> _stringToEnum;
        private static readonly Dictionary<T, string> _enumToString;

        static EnumMemberValueConverter()
        {
            _stringToEnum = new Dictionary<string, T>();
            _enumToString = new Dictionary<T, string>();

            foreach (var field in typeof(T).GetFields())
            {
                if (!field.IsLiteral) continue;

                var attr = field.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
                if (attr?.Value != null)
                {
                    var enumValue = (T)field.GetValue(null)!;
                    _stringToEnum[attr.Value] = enumValue;
                    _enumToString[enumValue] = attr.Value;
                }
            }
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert == typeof(T) || typeToConvert == typeof(T?);

        public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            if (_enumToString.TryGetValue((T)value, out var stringValue))
                writer.WriteStringValue(stringValue);
            else
                writer.WriteNullValue();
        }

        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException($"Expected string for enum, got {reader.TokenType}");

            var stringValue = reader.GetString();

            // Return the enum value if recognized, otherwise null for forward compatibility
            return stringValue != null && _stringToEnum.TryGetValue(stringValue, out var enumValue)
                ? enumValue
                : null;
        }
    }
}