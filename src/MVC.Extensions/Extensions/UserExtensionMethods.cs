using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MVC.Extensions.Exceptions;
using Newtonsoft.Json;

namespace MVC.Extensions.Extensions
{
	public static class UserExtensionMethods
	{
		/// <summary>
		/// 获取授权用户的数据信息（读取ClaimTypes.UserData）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller"></param>
		/// <param name="throwUnauthorizException">未授权时是否抛出异常</param>
		/// <param name="throwNullException">claim为空时是否抛出异常</param>
		/// <returns></returns>
		public static T GetAuthorizedUserData<T>(this ControllerBase controller, bool throwUnauthorizException = true, bool throwNullException = true)
		{
			var userdata = controller.GetAuthorizedClaimValue<T>(ClaimTypes.UserData, throwUnauthorizException, throwNullException);
			return userdata;
		}

		/// <summary>
		/// 获取当前授权用户的claim信息
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller"></param>
		/// <param name="claimName"></param>
		/// <param name="throwUnauthorizException">未授权时是否抛出异常</param>
		/// <param name="throwNullException">claim为空时是否抛出异常</param>
		/// <returns></returns>
		public static T GetAuthorizedClaimValue<T>(this ControllerBase controller, string claimName, bool throwUnauthorizException = true, bool throwNullException = true)
		{
			if (!controller?.User?.Identity?.IsAuthenticated ?? false)
				if (throwUnauthorizException)
					throw new HttpStatusCodeException(HttpStatusCode.Unauthorized);
				else
					return default(T);
			var user = controller?.User?.Claims?.ToList().FirstOrDefault(p => p.Type == claimName);
			if (string.IsNullOrWhiteSpace(user?.Value))
				if (throwNullException)
					throw new HttpStatusCodeException(HttpStatusCode.Forbidden);
				else
					return default(T);
			return JsonConvert.DeserializeObject<T>(user.Value);
		}
	}
}
