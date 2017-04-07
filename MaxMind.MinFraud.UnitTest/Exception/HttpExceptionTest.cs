#region

using System;
using System.Net;
using MaxMind.MinFraud.Exception;
using Xunit;

#endregion

namespace MaxMind.MinFraud.UnitTest.Exception
{
    public class HttpExceptionTest
    {
        [Fact]
        public void TestHttpException()
        {
            var url = new Uri("https://www.maxmind.com/");
            var e = new HttpException("message", HttpStatusCode.OK, url);
            Assert.Equal(HttpStatusCode.OK, e.HttpStatus);
            Assert.Equal(url, e.Uri);
        }
    }
}