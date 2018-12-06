using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MVC.Extensions.JWT
{
	public static class JwtBuilder
	{
		public static IServiceCollection AddJwtCookie(this IServiceCollection services, Action<CookieAuthenticationOptions> options)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options);
			return services;
		}

		public static IServiceCollection AddJwtCookie(this IServiceCollection services, string cookieName, string securityKey, bool validateIssuer = false, string validIssuer = "", bool validateAudience = false, string validAudience = "", string loginPath = "/account/login", string accessDeniedPath = "/account/accessDenied")
		{
			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
			var validationparamters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,
				ValidateIssuer = validateIssuer,
				ValidIssuer = validIssuer,
				ValidateAudience = validateAudience,
				ValidAudience = validAudience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.Cookie.Name = cookieName;
				options.TicketDataFormat = new JwtCookieDataFormat(SecurityAlgorithms.HmacSha512Signature, validationparamters);
				options.AccessDeniedPath = accessDeniedPath;
				options.LoginPath = loginPath;
			});
			return services;
		}

		public static IServiceCollection AddJwtBearer(this IServiceCollection services, string securityKey, bool validateIssuer = false, string validIssuer = "", bool validateAudience = false, string validAudience = "")
		{
			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
			var validationparamters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,
				ValidateIssuer = validateIssuer,
				ValidIssuer = validIssuer,
				ValidateAudience = validateAudience,
				ValidAudience = validAudience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = validationparamters;
			});
			return services;
		}
	}
}
