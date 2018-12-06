using System;
using System.Linq;

namespace MVC.Extensions.Extensions
{
	public static class RequestExtensionMethods
	{
		public static T Query<T>(this Microsoft.AspNetCore.Http.HttpRequest request, string key, T defaultValue = default(T)) where T : IConvertible
		{
			var r = request.Query.FirstOrDefault(p => p.Key == key);
			if (string.IsNullOrWhiteSpace(r.Value.ToString()))
				return defaultValue;
			try
			{
				return (T)Convert.ChangeType(r.Value.ToString(), typeof(T));
			}
			catch (InvalidCastException)
			{
				return defaultValue;
			}
		}

		public static T Form<T>(this Microsoft.AspNetCore.Http.HttpRequest request, string key, T defaultValue = default(T)) where T : IConvertible
		{
			var r = request.Form.FirstOrDefault(p => p.Key == key);
			if (string.IsNullOrWhiteSpace(r.Value.ToString()))
				return defaultValue;
			try
			{
				return (T)Convert.ChangeType(r.Value.ToString(), typeof(T));
			}
			catch (InvalidCastException)
			{
				return defaultValue;
			}
		}
	}
}
