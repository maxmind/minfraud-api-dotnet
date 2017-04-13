#region

using System;
using MaxMind.MinFraud.Exception;
using Xunit;

#endregion

namespace MaxMind.MinFraud.UnitTest.Exception
{
    public class InvalidRequestExceptionTest
    {
        [Fact]
        public void TestInvalidRequestException()
        {
            var url = new Uri("https://www.maxmind.com/");
            var code = "INVALID_INPUT";
            var e = new InvalidRequestException("message", code, url);
            Assert.Equal(code, e.Code);
            Assert.Equal(url, e.Uri);
        }
    }
}