using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVC.Extensions.Exceptions;
using System.Net;

namespace MVC.Extensions.Filters
{
	public class HttpGlobalExceptionFilter : IExceptionFilter
	{
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(HttpGlobalExceptionFilter));
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is HttpStatusCodeException exp)
			{
				context.Result = new ObjectResult(exp.Message) { StatusCode = exp.StatusCode };
			}
			else
			{
				Logger.Error(new
				{
					Title = "Catch global exception",
					Path = context.HttpContext.Request.Path.HasValue ? context.HttpContext.Request.Path.Value : "",
					context.Exception.Message,
					context.Exception.StackTrace
				});
				context.Result = new ObjectResult(context.Exception) { StatusCode = (int)HttpStatusCode.InternalServerError };
			}
			context.ExceptionHandled = true;
		}
	}
}
