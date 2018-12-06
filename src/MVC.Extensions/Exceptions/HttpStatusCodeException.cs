using System;
using System.Net;

namespace MVC.Extensions.Exceptions
{
	/// <inheritdoc />
	/// <summary>
	/// </summary>
	public class HttpStatusCodeException : Exception
	{
		public HttpStatusCodeException(int httpStatusCode) : base("")
		{
			StatusCode = httpStatusCode;
		}

		public HttpStatusCodeException(HttpStatusCode httpStatusCode) : base("")
		{
			StatusCode = (int)httpStatusCode;
		}

		public HttpStatusCodeException(int httpStatusCode, string message) : base(message)
		{
			StatusCode = httpStatusCode;
		}

		public HttpStatusCodeException(HttpStatusCode httpStatusCode, string message) : base(message)
		{
			StatusCode = (int)httpStatusCode;
		}

		public HttpStatusCodeException(int httpStatusCode, string message, Exception inner) : base(message, inner)
		{
			StatusCode = httpStatusCode;
		}

		public HttpStatusCodeException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
		{
			StatusCode = (int)httpStatusCode;
		}

		public int StatusCode { get; }
	}
}
