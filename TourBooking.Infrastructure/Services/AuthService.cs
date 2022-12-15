using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;
using TourBooking.Infrastructure.Helpers;

namespace TourBooking.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
	private readonly string secret;
	private readonly string issuer;
	private readonly string audience;
	private readonly DateTime accessTokenExpiry;
	private readonly DateTime refreshTokenExpiry;
	private readonly UserManager<ApplicationUser> userManager;

	public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
	{
		this.userManager = userManager;
		secret = configuration["JWT:Secret"] ?? throw new ArgumentNullException(nameof(secret), "JWT:Secret is missing from the appsettings.");
		issuer = configuration["JWT:Issuer"] ?? throw new ArgumentNullException(nameof(issuer), "JWT:Issuer is missing from the appsettings.");
		audience = configuration["JWT:Audience"] ?? throw new ArgumentNullException(nameof(audience), "JWT:Audience is missing from the appsettings.");
		accessTokenExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JWT:AccessTokenExpiryInMinutes"] ?? throw new ArgumentNullException(nameof(accessTokenExpiry), "JWT:AccessTokenExpiryInMinutes is missing from the appsettings.")));
		refreshTokenExpiry = DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["JWT:RefreshTokenExpiryInDays"] ?? throw new ArgumentNullException(nameof(refreshTokenExpiry), "JWT:RefreshTokenExpiryInDays is missing from the appsettings.")));
	}

	public async Task<ResponseDTO<TokenDTO>> LoginAsync(LoginInputModel loginInputModel)
	{
		var applicationUser = await userManager.FindByEmailAsync(loginInputModel.Email);

		if (applicationUser is null)
		{
			return new ResponseDTO<TokenDTO>(false, "Invalid email address.", LoginErrorType.CouldNotFindApplicationUser);
		}

		var isApplicationUserLockedOut = await userManager.IsLockedOutAsync(applicationUser);

		if (isApplicationUserLockedOut)
		{
			var dateTimeLockoutEnd = await userManager.GetLockoutEndDateAsync(applicationUser);
			var lockoutEnd = (dateTimeLockoutEnd - DateTimeOffset.Now).Value;
			var minutesLockoutEnd = Math.Ceiling(lockoutEnd.TotalMinutes);
			var secondsLockoutEnd = Math.Ceiling(lockoutEnd.TotalSeconds);
			var displayLockoutEndTime = minutesLockoutEnd <= 1 ? secondsLockoutEnd + " seconds" : minutesLockoutEnd + " minutes";

			return new ResponseDTO<TokenDTO>(false, $"You have attempted to login too many times. Try again in {displayLockoutEndTime}.", LoginErrorType.ApplicationUserIsLockedOut);
		}

		var isValidPassword = await userManager.CheckPasswordAsync(applicationUser, loginInputModel.Password);

		if (!isValidPassword)
		{
			await userManager.AccessFailedAsync(applicationUser);

			return new ResponseDTO<TokenDTO>(false, "Wrong password - try again.", LoginErrorType.InvalidPassword);
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()),
			new Claim(ClaimTypes.Name, applicationUser.UserName ?? throw new ArgumentNullException(nameof(applicationUser.UserName), "Attempted to login a user with no UserName.")),
			new Claim(ClaimTypes.Email, applicationUser.Email ?? throw new ArgumentNullException(nameof(applicationUser.Email), "Attempted to login a user with no Email.")),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var userRoles = await userManager.GetRolesAsync(applicationUser) as List<string>;

		if (userRoles?.Any() ?? false)
		{
			userRoles.ForEach(userRole => claims.Add(new Claim(ClaimTypes.Role, userRole)));
		}

		var accessToken = AuthHelper.GenerateAccessToken(claims, secret, issuer, audience, accessTokenExpiry);
		var refreshToken = AuthHelper.GenerateRefreshToken();

		applicationUser.RefreshToken = refreshToken;
		applicationUser.RefreshTokenExpiry = refreshTokenExpiry;

		var updateApplicationUserResult = await userManager.UpdateAsync(applicationUser);

		if (!updateApplicationUserResult.Succeeded)
		{
			return new ResponseDTO<TokenDTO>(false, "Server error. Unable to login - contact us for more information.", LoginErrorType.CouldNotUpdateApplicationUser, updateApplicationUserResult.Errors.FirstOrDefault()?.Description);
		}

		await userManager.ResetAccessFailedCountAsync(applicationUser);

		return new ResponseDTO<TokenDTO>(true, content: new TokenDTO(accessToken, refreshToken));
	}

	public async Task<ResponseDTO<TokenDTO>> RefreshTokensAsync(TokenDTO tokenModel)
	{
		if (string.IsNullOrWhiteSpace(tokenModel.AccessToken) || string.IsNullOrWhiteSpace(tokenModel.RefreshToken))
		{
			return new ResponseDTO<TokenDTO>(false, "Your session has expired - signing out...", RefreshTokensErrorType.AccessOrRefreshTokenIsNullOrWhitespace);
		}

		var claims = await AuthHelper.ValidateAccessTokenAsync(tokenModel.AccessToken, secret, issuer, audience);

		if (claims is null)
		{
			return new ResponseDTO<TokenDTO>(false, "Your session has expired - signing out...", RefreshTokensErrorType.CouldNotValidateAccessToken);
		}

		var email = claims.First(x => x.Type is ClaimTypes.Email).Value;
		var applicationUser = await userManager.FindByEmailAsync(email);

		if (applicationUser is null)
		{
			return new ResponseDTO<TokenDTO>(false, "Your session has expired - signing out...", RefreshTokensErrorType.CouldNotFindApplicationUser);
		}

		if (string.IsNullOrWhiteSpace(applicationUser.RefreshToken) || applicationUser.RefreshTokenExpiry is null || applicationUser.RefreshTokenExpiry <= DateTime.UtcNow)
		{
			return new ResponseDTO<TokenDTO>(false, "Your session has expired - signing out...", RefreshTokensErrorType.InvalidRefreshToken);
		}

		var newAccessToken = AuthHelper.GenerateAccessToken(claims, secret, issuer, audience, accessTokenExpiry);
		var newRefreshToken = AuthHelper.GenerateRefreshToken();

		applicationUser.RefreshToken = newRefreshToken;
		applicationUser.RefreshTokenExpiry = refreshTokenExpiry;

		var updateApplicationUserResult = await userManager.UpdateAsync(applicationUser);

		if (!updateApplicationUserResult.Succeeded)
		{
			return new ResponseDTO<TokenDTO>(false, "Your session has expired - signing out...", RefreshTokensErrorType.CouldNotUpdateApplicationUser, updateApplicationUserResult.Errors.FirstOrDefault()?.Description);
		}

		return new ResponseDTO<TokenDTO>(true, content: new TokenDTO(newAccessToken, newRefreshToken));
	}
}