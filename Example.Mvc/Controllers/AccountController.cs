using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Example.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using MVC.Extensions.JWT;
using Newtonsoft.Json.Linq;
using MVC.Extensions.Extensions;
using Newtonsoft.Json;

namespace Example.Mvc.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost, AutoValidateAntiforgeryToken]
		public IActionResult Login([FromForm] Login form)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new {form.UserName}))
			};
			var token = JwtToken.GenerateToken(claims, "99a62aa2-df24-4a7a-b418-68aa4f332369", TimeSpan.FromMinutes(2));

			Response.Cookies.Append("access_token", token,
				new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTimeOffset.Now.AddMinutes(2)
				});
			var returnUrl = Request.Query<string>("ReturnUrl");
			return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
		}
	}
}