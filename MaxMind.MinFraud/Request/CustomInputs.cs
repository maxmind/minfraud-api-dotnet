using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    ///     Custom inputs to be used in
    ///     <a href="https://www.maxmind.com/en/minfraud-interactive/#/custom-rules">Custom Rules</a>.
    ///     In order to use custom inputs, you must set them up from your account portal.
    /// </summary>
    public sealed class CustomInputs
    {
        private readonly Dictionary<string, object> _inputs = new Dictionary<string, object>();

        /// <summary>
        ///     This is only to be used by the Builder.
        /// </summary>
        private CustomInputs()
        {
        }

        /// <summary>
        ///     The custom inputs to be sent to the web service.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> Inputs => new ReadOnlyDictionary<string, object>(_inputs);

        /// <summary>
        /// Builder class for <code>CustomInputs</code> objects.
        /// </summary>
        public sealed class Builder : IEnumerable<KeyValuePair<string, object>>
        {
            private const long NumMax = (long) 1e13;
            private static readonly Regex KeyRe = new Regex("^[a-z0-9_]{1,25}$", RegexOptions.Compiled);

            // We do the builder this way so that we don't have to
            // make a copy of the dictionary after construction
            private readonly CustomInputs _customInputs = new CustomInputs();
            private bool alreadyBuilt = false;

            /// <summary>
            ///     Returns an enumerator that iterates through the inputs.
            /// </summary>
            public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
            {
                ValidateState();
                return _customInputs._inputs.GetEnumerator();
            }

            /// <summary>
            ///     Returns an enumerator that iterates through the inputs.
            /// </summary>
            IEnumerator IEnumerable.GetEnumerator()
            {
                ValidateState();
                return GetEnumerator();
            }

            /// <summary>
            ///     Build the <code>CustomInputs</code> object.
            /// </summary>
            /// <returns>The constructed <code>CustomInputs</code> object.</returns>
            public CustomInputs Build()
            {
                ValidateState();

                var customInputs = _customInputs;

                // Invalidate the builder so additional inputs cannot be
                // added.
                alreadyBuilt = true;

                return customInputs;
            }

            /// <summary>
            ///     Add an int as a numeric custom input.
            /// </summary>
            /// <param name="key">The key for the numeric input defined in your account portal.</param>
            /// <param name="value">The input value.</param>
            public void Add(string key, int value)
            {
                ValidatedAdd(key, value);
            }

            /// <summary>
            ///     Add a long as a numeric custom input.
            /// </summary>
            /// <param name="key">The key for the numeric input defined in your account portal.</param>
            /// <param name="value">
            ///     The input. The value must be between -10^13 and 10^13,
            ///     exclusive.
            /// </param>
            public void Add(string key, long value)
            {
                if (value <= -NumMax || value >= NumMax)
                    throw new ArgumentException($"The custom input number {value} is invalid. " +
                                                $"The number must be between -{NumMax} and {NumMax}, exclusive");
                ValidatedAdd(key, value);
            }

            /// <summary>
            ///     Add a float as a numeric custom input.
            /// </summary>
            /// <param name="key">The key for the numeric input defined in your account portal.</param>
            /// <param name="value">
            ///     The input value. The value must be between -10^13 and 10^13,
            ///     exclusive.
            /// </param>
            public void Add(string key, float value)
            {
                if (value <= -NumMax || value >= NumMax)
                    throw new ArgumentException($"The custom input number {value} is invalid. " +
                                                $"The number must be between -{NumMax} and {NumMax}, exclusive");
                ValidatedAdd(key, value);
            }

            /// <summary>
            ///     Add a double as a numeric custom input.
            /// </summary>
            /// <param name="key">The key for the numeric input defined in your account portal.</param>
            /// <param name="value">
            ///     The input value. The value must be between -10^13 and 10^13,
            ///     exclusive.
            /// </param>
            public void Add(string key, double value)
            {
                if (value <= -NumMax || value >= NumMax)
                    throw new ArgumentException($"The custom input number {value} is invalid. " +
                                                $"The number must be between -{NumMax} and {NumMax}, exclusive");
                ValidatedAdd(key, value);
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
                if (value.Length > 255 || value.Contains("\n"))
                    throw new ArgumentException($"The custom input string {value} is invalid. " +
                                                "The string length must be <= 255 and it must not contain a newline.");
                ValidatedAdd(key, value);
            }

            /// <summary>
            ///     Add a boolean custom input.
            /// </summary>
            /// <param name="key">The key for the boolean input defined in your account portal.</param>
            /// <param name="value">The input value.</param>
            public void Add(string key, bool value)
            {
                ValidatedAdd(key, value);
            }

            private void ValidatedAdd(string key, object value)
            {
                ValidateState();
                ValidateKey(key);
                _customInputs._inputs.Add(key, value);
            }

            private static void ValidateKey(string key)
            {
                if (!KeyRe.IsMatch(key))
                    throw new ArgumentException($"The custom input key {key} is invalid.");
            }

            private void ValidateState()
            {
                if (alreadyBuilt)
                    throw new InvalidOperationException(
                        "CustomInputs.Builder cannot be reused after Build() has been called.");
            }
        }
    }
}
