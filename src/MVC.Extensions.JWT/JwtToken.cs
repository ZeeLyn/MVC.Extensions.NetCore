using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MVC.Extensions.JWT
{
	public class JwtToken
	{
		/// <summary>
		/// 生成jwt
		/// </summary>
		/// <param name="claims">claims</param>
		/// <param name="securityKey">加密key，需与验证授权的key一致</param>
		/// <param name="expire">过期时间</param>
		/// <param name="issuer">发行人</param>
		/// <param name="audience">受众群体</param>
		/// <returns></returns>
		public static string GenerateToken(List<Claim> claims, string securityKey, TimeSpan expire, string issuer = "", string audience = "")
		{
			claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
			var now = DateTime.UtcNow;
			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
				SecurityAlgorithms.HmacSha512Signature);
			var jwtHeader = new JwtHeader(signingCredentials);
			var jwtPayload = new JwtPayload(
				 issuer,
				 audience,
				 claims,
				 now,
				 now.Add(expire),
				 now);
			var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);
			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}
