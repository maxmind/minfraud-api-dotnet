#region

using System;
using System.Net;
using MaxMind.MinFraud.Exception;
using NUnit.Framework;

#endregion

namespace MaxMind.MinFraud.UnitTest.Exception
{
    public class HttpExceptionTest
    {
        [Test]
        public void TestHttpException()
        {
            var url = new Uri("https://www.maxmind.com/");
            var e = new HttpException("message", HttpStatusCode.OK, url);
            Assert.AreEqual(HttpStatusCode.OK, e.HttpStatus);
            Assert.AreEqual(url, e.Uri);
        }
    }
}