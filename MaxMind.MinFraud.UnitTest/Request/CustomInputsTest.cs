using MaxMind.MinFraud.Request;
using System;
using System.Text.Json;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CustomInputsTest
    {
        [Fact]
        public void TestJson()
        {
            var inputs = new CustomInputs.Builder
            {
                {"string_input_1", "test string"},
                {"int_input", 19},
                {"long_input", 12L},
                {"float_input", 3.2f},
                {"double_input", 32.123d},
                {"bool_input", true}
            }.Build();

            var json = JsonSerializer.Serialize(inputs);
            var comparer = new JsonElementComparer();
            Assert.True(comparer.JsonEquals(
                JsonDocument.Parse(
                    """
                    {
                        "string_input_1": "test string",
                        "int_input": 19,
                        "long_input": 12,
                        "float_input": 3.20000005,
                        "double_input": 32.122999999999998,
                        "bool_input": true
                    }
                    """),
                JsonDocument.Parse(json)
                ),
                json
            );
        }

        [Fact]
        public void TestStringThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"string_input_1", new string('x', 256)}
            }.Build());
        }

        [Fact]
        public void TestStringWithNewLine()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"string", "test\n"}
            }.Build());
        }

        [Fact]
        public void TestInvalidKey()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"InvalidKey", "test"}
            }.Build());
        }

        [Fact]
        public void TestInvalidLong()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"invalid_long", (long) 1e13}
            });
        }

        [Fact]
        public void TestInvalidFloat()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"invalid_float", (float) 1e13}
            }.Build());
        }

        [Fact]
        public void TestInvalidDouble()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs.Builder
            {
                {"invalid_double", (double) -1e13}
            }.Build());
        }

        [Fact]
        public void TestBuilderCannotBeReused()
        {
            var builder = new CustomInputs.Builder
            {
                {"string", "test"}
            };

            builder.Build();

            Assert.Throws<InvalidOperationException>(() => builder.Add("nope", true));
            Assert.Throws<InvalidOperationException>(() => builder.Build());
            Assert.Throws<InvalidOperationException>(() => builder.GetEnumerator());
        }
    }
}
