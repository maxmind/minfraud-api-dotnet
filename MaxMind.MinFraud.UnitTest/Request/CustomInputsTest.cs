using System;
using MaxMind.MinFraud.Request;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CustomInputsTest
    {
        [Fact]
        public void TestJson()
        {
            var inputs = new CustomInputs
            {
                {"string_input_1", "test string"},
                {"int_input", 19},
                {"long_input", 12L},
                {"float_input", 3.2f},
                {"double_input", 32.123d},
                {"bool_input", true}
            };

            Assert.Equal(
                new JObject
                {
                    {"string_input_1", "test string"},
                    {"int_input", 19},
                    {"long_input", 12L},
                    {"float_input", 3.2f},
                    {"double_input", 32.123d},
                    {"bool_input", true}
                },
                JToken.FromObject(inputs));
        }

        [Fact]
        public void TestStringThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"string_input_1", new string('x', 256)}
                });
        }

        [Fact]
        public void TestStringWithNewLine()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"string", "test\n"}
                });
        }

        [Fact]
        public void TestInvalidKey()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"InvalidKey", "test"}
                });
        }

        [Fact]
        public void TestInvalidLong()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"invalid_long", 1L << 54}
                });
        }

        [Fact]
        public void TestInvalidFloat()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"invalid_float", (float) (1L << 54)}
                });
        }

        [Fact]
        public void TestInvalidDouble()
        {
            Assert.Throws<ArgumentException>(() => new CustomInputs
                {
                    {"invalid_double", (double) (-1L << 54)}
                });
        }
    }
}