using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Extensions.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class PowersAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		/// <summary>
		/// 权限集，多个权限用逗号隔开(all:表示拥有所有权限，none：表示没有任何权限)
		/// </summary>
		public string Powers { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (string.IsNullOrEmpty(Powers))
				return;
			var user = context.HttpContext.User;
			if (!user.Identity.IsAuthenticated)
			{
				context.Result = new StatusCodeResult((int)HttpStatusCode.Unauthorized);
				return;
			}

			var claimpowers = user.Claims.FirstOrDefault(p => p.Type == "Powers")?.Value ?? "";
			if (string.IsNullOrWhiteSpace(claimpowers) || claimpowers.Equals("none", StringComparison.CurrentCultureIgnoreCase))
			{
				context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
				return;
			}

			if (!claimpowers.Equals("all", StringComparison.CurrentCultureIgnoreCase))
			{
				var currepowers = Powers.Split(',');
				var haspower = claimpowers.Split(',');
				if (!currepowers.Any(p => haspower.Contains(p)))
				{
					context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
				}
			}
		}
	}
}
