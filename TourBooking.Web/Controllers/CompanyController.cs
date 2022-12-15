using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Route("api/company")]
[ApiController]
public class CompanyController : EFBaseController<Company>
{
	private readonly ICompanyService companyService;

	public CompanyController(ICompanyService companyService) : base(companyService)
	{
		this.companyService = companyService;
	}

	[HttpPost("RegisterCompany"), Authorize(Roles = "Admin")]
	public async Task<ActionResult<ResponseDTO<Company>>> RegisterCompanyAsync(RegisterCompanyInputModel registerCompanyInputModel, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await companyService.RegisterCompanyAsync(registerCompanyInputModel, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is RegisterCompanyErrorType.CouldNotCreateCompany or RegisterCompanyErrorType.CouldNotCreateHeadquarter or RegisterCompanyErrorType.CouldNotCreatePrimaryTheme)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("LoadCompanyTheme"), AllowAnonymous]
	public async Task<ActionResult<ResponseDTO<CompanyThemeDTO>>> LoadCompanyThemeAsync(string handle, CancellationToken cancellationToken)
	{
		var result = await companyService.LoadCompanyThemeAsync(handle, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is LoadCompanyThemeErrorType.CouldNotFindCompany)
			{
				return NotFound(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("GetCompanyList"), Authorize(Roles = "Admin")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<CompanyListDTO>>>> GetCompanyListAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetCompanyListAsync(cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}
}