﻿using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// A warning returned by the web service.
    /// </summary>
    public sealed class Warning
    {
        /// <summary>
        /// This value is a machine-readable code identifying the
        /// warning. See the <a href="https://dev.maxmind.com/minfraud/api-documentation/responses?lang=en#schema--response--warning">
        /// web service documentation</a> for the current list of warning
        ///  codes.
        ///</summary>
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        /// <summary>
        /// This property provides a human-readable explanation of the
        /// warning. The description may change at any time and should not be
        /// matched against.
        /// </summary>
        [JsonPropertyName("warning")]
        public string? Message { get; init; }

        /// <summary>
        /// A JSON Pointer to the input field that the warning is associated with.
        /// For instance, if the warning was about the billing city, this would be
        /// <c>/billing/city</c>. If it was for the price in the second shopping
        /// cart item, it would be <c>/shopping_cart/1/price</c>.
        /// </summary>
        [JsonPropertyName("input_pointer")]
        public string? InputPointer { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}, InputPointer: {InputPointer}";
        }
    }
}
