using System;
using MaxMind.MinFraud.Request;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CustomInputsTest
    {
        [Test]
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

            Assert.AreEqual(
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

        [Test]
        public void TestStringThatIsTooLong()
        {
            Assert.That(() => new CustomInputs
                {
                    {"string_input_1", new string('x', 256)}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }

        [Test]
        public void TestStringWithNewLine()
        {
            Assert.That(() => new CustomInputs
                {
                    {"string", "test\n"}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }

        [Test]
        public void TestInvalidKey()
        {
            Assert.That(() => new CustomInputs
                {
                    {"InvalidKey", "test"}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }

        [Test]
        public void TestInvalidLong()
        {
            Assert.That(() => new CustomInputs
                {
                    {"invalid_long", 1L << 54}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }

        [Test]
        public void TestInvalidFloat()
        {
            Assert.That(() => new CustomInputs
                {
                    {"invalid_float", (float) (1L << 54)}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }

        [Test]
        public void TestInvalidDouble()
        {
            Assert.That(() => new CustomInputs
                {
                    {"invalid_double", (double) (-1L << 54)}
                },
                Throws.TypeOf<ArgumentException>(
                ));
        }
    }
}