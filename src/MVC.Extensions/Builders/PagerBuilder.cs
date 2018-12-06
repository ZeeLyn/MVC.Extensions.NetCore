using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace MVC.Extensions.Builders
{
	public static class PagerBuilder
	{
		public static IServiceCollection AddPaging(this IServiceCollection service)
		{
			service.Configure<RazorViewEngineOptions>(options =>
			{
				options.FileProviders.Add(new EmbeddedFileProvider(typeof(Extensions.MvcExtensionMethods).GetTypeInfo().Assembly));
			});
			return service;
		}
	}
}
