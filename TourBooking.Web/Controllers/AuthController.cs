using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Route("api/auth")]
[ApiController]
public sealed class AuthController : ControllerBase
{
	private readonly IAuthService authService;

	public AuthController(IAuthService authService)
	{
		this.authService = authService;
	}

	[HttpPost("Login"), AllowAnonymous]
	public async Task<ActionResult<ResponseDTO<TokenDTO>>> LoginAsync(LoginInputModel loginInputModel)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await authService.LoginAsync(loginInputModel);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is LoginErrorType.CouldNotFindApplicationUser)
			{
				return NotFound(result);
			}
			else if (result.ErrorType is LoginErrorType.InvalidPassword)
			{
				return BadRequest(result);
			}
			else if (result.ErrorType is LoginErrorType.ApplicationUserIsLockedOut or LoginErrorType.CouldNotUpdateApplicationUser)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPost("RefreshToken"), AllowAnonymous]
	public async Task<ActionResult<ResponseDTO<TokenDTO>>> RefreshTokenAsync(TokenDTO tokenModel)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await authService.RefreshTokensAsync(tokenModel);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is RefreshTokensErrorType.CouldNotFindApplicationUser)
			{
				return NotFound(result);
			}
			else if (result.ErrorType is RefreshTokensErrorType.AccessOrRefreshTokenIsNullOrWhitespace or RefreshTokensErrorType.InvalidRefreshToken)
			{
				return BadRequest(result);
			}
			else if (result.ErrorType is RefreshTokensErrorType.CouldNotValidateAccessToken or RefreshTokensErrorType.CouldNotUpdateApplicationUser)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}
}