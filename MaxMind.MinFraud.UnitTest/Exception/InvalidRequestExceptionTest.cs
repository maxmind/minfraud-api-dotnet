#region

using System;
using MaxMind.MinFraud.Exception;
using NUnit.Framework;

#endregion

namespace MaxMind.MinFraud.UnitTest.Exception
{
    public class InvalidRequestExceptionTest
    {
        [Test]
        public void TestInvalidRequestException()
        {
            var url = new Uri("https://www.maxmind.com/");
            var code = "INVALID_INPUT";
            var e = new InvalidRequestException("message", code, url);
            Assert.AreEqual(code, e.Code);
            Assert.AreEqual(url, e.Uri);
        }
    }
}