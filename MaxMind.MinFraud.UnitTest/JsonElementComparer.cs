using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MaxMind.MinFraud.UnitTest
{
    // This is taken from https://stackoverflow.com/questions/60580743/what-is-equivalent-in-jtoken-deepequal-in-system-text-json
    //
    // Per https://stackoverflow.com/help/licensing, it is licensed under CC BY-SA 4.0.
    //
    // Hopefully a future version of System.Text.Json will include this functionality.
    public class JsonElementComparer : IEqualityComparer<JsonElement>
    {
        public JsonElementComparer() : this(-1) { }

        public JsonElementComparer(int maxHashDepth)
        {
            MaxHashDepth = maxHashDepth;
        }

        private int MaxHashDepth { get; }

        #region IEqualityComparer<JsonElement> Members

        public bool JsonEquals(JsonDocument x, JsonDocument y)
        {
            return Equals(x.RootElement, y.RootElement);
        }

        public bool Equals(JsonElement x, JsonElement y)
        {
            if (x.ValueKind != y.ValueKind)
                return false;
            switch (x.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Undefined:
                    return true;

                case JsonValueKind.Number:
                    return Math.Abs(x.GetDouble() - y.GetDouble()) < 0.0001;

                case JsonValueKind.String:
                    return x.GetString() == y.GetString(); // Do not use GetRawText() here, it does not automatically resolve JSON escape sequences to their corresponding characters.

                case JsonValueKind.Array:
                    return x.EnumerateArray().SequenceEqual(y.EnumerateArray(), this);

                case JsonValueKind.Object:
                    {
                        // Surprisingly, JsonDocument fully supports duplicate property names.
                        // I.e. it's perfectly happy to parse {"Value":"a", "Value" : "b"} and will store both
                        // key/value pairs inside the document!
                        // A close reading of https://tools.ietf.org/html/rfc8259#section-4 seems to indicate that
                        // such objects are allowed but not recommended, and when they arise, interpretation of 
                        // identically-named properties is order-dependent.  
                        // So stably sorting by name then comparing values seems the way to go.
                        var xPropertiesUnsorted = x.EnumerateObject().ToList();
                        var yPropertiesUnsorted = y.EnumerateObject().ToList();
                        if (xPropertiesUnsorted.Count != yPropertiesUnsorted.Count)
                            return false;
                        var xProperties = xPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
                        var yProperties = yPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
                        foreach (var (px, py) in xProperties.Zip(yProperties, (x, y) => (x, y)))
                        {
                            if (px.Name != py.Name)
                                return false;
                            if (!Equals(px.Value, py.Value))
                                return false;
                        }
                        return true;
                    }

                default:
                    throw new JsonException($"Unknown JsonValueKind {x.ValueKind}");
            }
        }

        public int GetHashCode(JsonElement obj)
        {
            var hash = new HashCode(); // New in .Net core: https://docs.microsoft.com/en-us/dotnet/api/system.hashcode
            ComputeHashCode(obj, ref hash, 0);
            return hash.ToHashCode();
        }

        private void ComputeHashCode(JsonElement obj, ref HashCode hash, int depth)
        {
            hash.Add(obj.ValueKind);

            switch (obj.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Undefined:
                    break;

                case JsonValueKind.Number:
                    hash.Add(obj.GetRawText());
                    break;

                case JsonValueKind.String:
                    hash.Add(obj.GetString());
                    break;

                case JsonValueKind.Array:
                    if (depth != MaxHashDepth)
                    {
                        foreach (var item in obj.EnumerateArray())
                            ComputeHashCode(item, ref hash, depth + 1);
                    }
                    else
                    {
                        hash.Add(obj.GetArrayLength());
                    }
                    break;

                case JsonValueKind.Object:
                    foreach (var property in obj.EnumerateObject().OrderBy(p => p.Name, StringComparer.Ordinal))
                    {
                        hash.Add(property.Name);
                        if (depth != MaxHashDepth)
                            ComputeHashCode(property.Value, ref hash, depth + 1);
                    }
                    break;

                default:
                    throw new JsonException($"Unknown JsonValueKind {obj.ValueKind}");
            }
        }

        #endregion
    }
}
