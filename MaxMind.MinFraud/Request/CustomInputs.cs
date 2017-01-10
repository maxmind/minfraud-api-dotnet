using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    ///     Custom inputs to be used in
    ///     <a href="https://www.maxmind.com/en/minfraud-interactive/#/custom-rules">Custom Rules</a>.
    ///     In order to use custom inputs, you must set them up from your account portal.
    /// </summary>
    [JsonObject]
    public sealed class CustomInputs : IEnumerable<KeyValuePair<string, object>>
    {
        private const long NumMax = 1 << 53;
        private static readonly Regex KeyRe = new Regex("^[a-z0-9_]{1,25}$", RegexOptions.Compiled);

        [JsonExtensionData] private readonly Dictionary<string, object> _inputs = new Dictionary<string, object>();


        /// <summary>
        ///     Returns an enumerator that iterates through the inputs.
        /// </summary>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _inputs.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the inputs.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Add an int as a numeric custom input.
        /// </summary>
        /// <param name="key">The key for the numeric input defined in your account portal.</param>
        /// <param name="value">The input value.</param>
        public void Add(string key, int value)
        {
            validateKey(key);
            _inputs.Add(key, value);
        }

        /// <summary>
        ///     Add a long as a numeric custom input.
        /// </summary>
        /// <param name="key">The key for the numeric input defined in your account portal.</param>
        /// <param name="value">
        ///     The input. The value must be between -2^53 and 2^53,
        ///     exclusive.
        /// </param>
        public void Add(string key, long value)
        {
            validateKey(key);
            if (value <= -NumMax || value >= NumMax)
                throw new ArgumentException($"The custom input number {value} is invalid. " +
                                            $"The number must be between -{NumMax} and {NumMax}, exclusive");
            _inputs.Add(key, value);
        }

        /// <summary>
        ///     Add a float as a numeric custom input.
        /// </summary>
        /// <param name="key">The key for the numeric input defined in your account portal.</param>
        /// <param name="value">
        ///     The input value. The value must be between -2^53 and 2^53,
        ///     exclusive.
        /// </param>
        public void Add(string key, float value)
        {
            validateKey(key);
            if (value <= -NumMax || value >= NumMax)
                throw new ArgumentException($"The custom input number {value} is invalid. " +
                                            $"The number must be between -{NumMax} and {NumMax}, exclusive");
            _inputs.Add(key, value);
        }

        /// <summary>
        ///     Add a double as a numeric custom input.
        /// </summary>
        /// <param name="key">The key for the numeric input defined in your account portal.</param>
        /// <param name="value">
        ///     The input value. The value must be between -2^53 and 2^53,
        ///     exclusive.
        /// </param>
        public void Add(string key, double value)
        {
            validateKey(key);
            if (value <= -NumMax || value >= NumMax)
                throw new ArgumentException($"The custom input number {value} is invalid. " +
                                            $"The number must be between -{NumMax} and {NumMax}, exclusive");
            _inputs.Add(key, value);
        }

        /// <summary>
        ///     Add a custom input string.
        /// </summary>
        /// <param name="key">The key for the string input defined in your account portal.</param>
        /// <param name="value">
        ///     The input value. The string length must be less than or equal to 255
        ///     characters and the string must not contain a newline character.
        /// </param>
        public void Add(string key, string value)
        {
            validateKey(key);
            if (value.Length > 255 || value.Contains("\n"))
                throw new ArgumentException($"The custom input string {value} is invalid. " +
                                            "The string length must be <= 255 and it must not contain a newline.");
            _inputs.Add(key, value);
        }

        /// <summary>
        ///     Add a boolean custom input.
        /// </summary>
        /// <param name="key">The key for the boolean input defined in your account portal.</param>
        /// <param name="value">The input value.</param>
        public void Add(string key, bool value)
        {
            validateKey(key);
            _inputs.Add(key, value);
        }

        private void validateKey(string key)
        {
            if (!KeyRe.IsMatch(key))
                throw new ArgumentException($"The custom input key {key} is invalid.");
        }
    }
}