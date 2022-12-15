using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Helpers;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Route("api/nomi4s")]
[ApiController]
public class Nomi4sController : ControllerBase
{
	private readonly ICompanyService companyService;
	private readonly IBookingService bookingService;

	public Nomi4sController(ICompanyService companyService, IBookingService bookingService)
	{
		this.companyService = companyService;
		this.bookingService = bookingService;
	}

	[HttpPost("CreateLocation"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<Location>>> CreateLocationAsync(CreateLocationInputModel createLocationInputModel, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await companyService.CreateLocationAsync(createLocationInputModel, cancellationToken);

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
			else if (result.ErrorType is CreateLocationErrorType.CouldNotCreateLocation)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPost("CreatePackage"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<Package>>> CreatePackageAsync(CreatePackageInputModel createPackageInputModel, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await companyService.CreatePackageAsync(createPackageInputModel, cancellationToken);

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
			else if (result.ErrorType is CreatePackageErrorType.CouldNotCreatePackage)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPost("CreateMaterial"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<Material>>> CreateMaterialAsync(CreateMaterialInputModel createMaterialInputModel, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			return ValidationProblem();
		}

		var result = await companyService.CreateMaterialAsync(createMaterialInputModel, cancellationToken);

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
			else if (result.ErrorType is CreateMaterialErrorType.CouldNotCreateMaterial)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("GetCompanyDetails"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<CompanyDetailsDTO>>> GetCompanyDetailsAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetCompanyDetailsAsync(CompanyHandleList.Nomi4s.EnumToString(), cancellationToken);

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
			else if (result.ErrorType is GetCompanyDetailsErrorType.CouldNotFindCompany)
			{
				return NotFound(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("GetPackageList"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<CompanyPackageListDTO>>>> GetCompanyPackageListAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetCompanyPackageListAsync(CompanyHandleList.Nomi4s.EnumToString(), cancellationToken);

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

	[HttpGet("GetLocationList"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<CompanyLocationListDTO>>>> GetCompanyLocationListAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetCompanyLocationListAsync(CompanyHandleList.Nomi4s.EnumToString(), cancellationToken);

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

	[HttpGet("GetMaterialList"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<CompanyMaterialListDTO>>>> GetCompanyMaterialListAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetCompanyMaterialListAsync(CompanyHandleList.Nomi4s.EnumToString(), cancellationToken);

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

	[HttpGet("GetBookingList"), Authorize]
	public async Task<ActionResult<ResponseDTO<IEnumerable<BookingListDTO>>>> GetBookingListAsync(CancellationToken cancellationToken)
	{
		var result = User.IsInRole(RoleType.Admin.ToString()) || User.IsInRole(CompanyHandleList.Nomi4s.EnumToString())
			? await bookingService.GetBookingListAsync(CompanyHandleList.Nomi4s.EnumToString(), null, cancellationToken)
			: await bookingService.GetBookingListAsync(CompanyHandleList.Nomi4s.EnumToString(), Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.NewGuid().ToString()), cancellationToken);

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

	[HttpGet("GetBookingDetails/{id}"), Authorize]
	public async Task<ActionResult<ResponseDTO<BookingDetailsDTO>>> GetBookingDetailsAsync(Guid id, CancellationToken cancellationToken)
	{
		var result = User.IsInRole(RoleType.Admin.ToString()) || User.IsInRole(CompanyHandleList.Nomi4s.EnumToString())
			? await bookingService.GetBookingDetailsAsync(id, null, cancellationToken)
			: await bookingService.GetBookingDetailsAsync(id, Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.NewGuid().ToString()), cancellationToken);

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
			else if (result.ErrorType is GetBookingDetailsErrorType.CouldNotFindBooking)
			{
				return NotFound(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPut("ToggleBookingStatus/{id}/{bookingStatus}"), Authorize]
	public async Task<ActionResult<ResponseDTO<BookingDetailsDTO>>> ToggleBookingStatusAsync(Guid id, BookingStatus bookingStatus, CancellationToken cancellationToken)
	{
		var result = User.IsInRole(RoleType.Admin.ToString()) || User.IsInRole(CompanyHandleList.Nomi4s.EnumToString())
			? await bookingService.ToggleBookingStatusAsync(id, bookingStatus, null, cancellationToken)
			: await bookingService.ToggleBookingStatusAsync(id, BookingStatus.Closed, Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.NewGuid().ToString()), cancellationToken);

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
			else if (result.ErrorType is ToggleBookingStatusErrorType.CouldNotUpdateBooking)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPost("CreateMessage"), Authorize]
	public async Task<ActionResult<ResponseDTO<Message>>> CreateMessageAsync(CreateMessageInputModel createMessageInputModel, CancellationToken cancellationToken)
	{
		var result = User.IsInRole(RoleType.Admin.ToString()) || User.IsInRole(CompanyHandleList.Nomi4s.EnumToString())
			? await bookingService.CreateMessageAsync(createMessageInputModel, null, cancellationToken)
			: await bookingService.CreateMessageAsync(createMessageInputModel, Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.NewGuid().ToString()), cancellationToken);

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
			else if (result.ErrorType is CreateMessageErrorType.CouldNotFindBooking)
			{
				return NotFound(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("GetAllEmployeesEmailList"), Authorize(Roles = "nomi4s, Admin")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<EmployeesEmailListDTO>>>> GetAllEmployeesEmailListAsync(CancellationToken cancellationToken)
	{
		var result = await companyService.GetAllEmployeesEmailListAsync(CompanyHandleList.Nomi4s.EnumToString(), cancellationToken);

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