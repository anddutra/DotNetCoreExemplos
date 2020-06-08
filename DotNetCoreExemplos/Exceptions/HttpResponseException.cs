using System;

namespace DotNetCoreExemplos.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; set; }
        public HttpResponseException(int statusCode, string message) : this(statusCode, message, null)
        {
        }

        public HttpResponseException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}