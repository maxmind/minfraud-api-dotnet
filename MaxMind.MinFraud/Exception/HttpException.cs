﻿#region

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

#endregion

namespace MaxMind.MinFraud.Exception
{
    /// <summary>
    ///     This class represents an HTTP transport error. This is not an error returned
    ///     by the web service itself. As such, it is a IOException instead of a
    ///     MinFraudException.
    /// </summary>
    [Serializable]
    public class HttpException : IOException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HttpException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">A message describing the reason why the exception was thrown.</param>
        public HttpException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public HttpException(string? message, int hresult) : base(message, hresult)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">A message describing the reason why the exception was thrown.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public HttpException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="message">A message describing the reason why the exception was thrown.</param>
        /// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
        /// <param name="uri">The URL queried.</param>
        public HttpException(string message, HttpStatusCode httpStatus, Uri? uri)
            : base(message)
        {
            HttpStatus = httpStatus;
            Uri = uri;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="message">A message describing the reason why the exception was thrown.</param>
        /// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
        /// <param name="uri">The URL queried.</param>
        /// <param name="innerException">The underlying exception that caused this one.</param>
        public HttpException(string message, HttpStatusCode httpStatus, Uri? uri, System.Exception innerException)
            : base(message, innerException)
        {
            HttpStatus = httpStatus;
            Uri = uri;
        }

        /// <summary>
        ///     The HTTP status code returned by the web service.
        /// </summary>
        public HttpStatusCode? HttpStatus { get; init; }

        /// <summary>
        ///     The URI queried by the web service.
        /// </summary>
        public Uri? Uri { get; init; }
    }
}
