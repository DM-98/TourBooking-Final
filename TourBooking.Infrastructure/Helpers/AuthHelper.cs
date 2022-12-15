using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TourBooking.Infrastructure.Helpers;

public static class AuthHelper
{
	public static string GenerateAccessToken(IEnumerable<Claim> claims, string secret, string issuer, string audience, DateTime accessTokenExpiry)
	{
		var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
		var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
		var jwtSecurityToken = new JwtSecurityToken(issuer: issuer, audience: !claims.Any(x => x.Type is JwtRegisteredClaimNames.Aud) ? audience : null, claims: claims, expires: accessTokenExpiry, signingCredentials: signingCredentials);
		var accesstoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

		return accesstoken;
	}

	public static string GenerateRefreshToken()
	{
		var random = new byte[64];

		using var randomNumberGenerator = RandomNumberGenerator.Create();
		randomNumberGenerator.GetBytes(random);

		var refreshToken = Convert.ToBase64String(random);

		return refreshToken;
	}

	public static async Task<IEnumerable<Claim>?> ValidateAccessTokenAsync(string accessToken, string secret, string issuer, string audience)
	{
		var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			ValidateLifetime = false,
			IssuerSigningKey = symmetricSecurityKey,
			ValidIssuer = issuer,
			ValidAudience = audience,
			ClockSkew = TimeSpan.Zero
		};

		var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(accessToken, tokenValidationParameters);

		if (tokenValidationResult.SecurityToken is JwtSecurityToken jwtSecurityToken)
		{
			if (jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.OrdinalIgnoreCase))
			{
				return tokenValidationResult.ClaimsIdentity.Claims;
			}
		}

		return null;
	}

	public static IEnumerable<Claim> GetClaims(string jwt)
	{
		return new JwtSecurityTokenHandler().ReadJwtToken(jwt).Claims;
	}
}