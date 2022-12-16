using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("RegisterAdmin"), AllowAnonymous]
    public async Task<ActionResult<ResponseDTO<Admin>>> RegisterAdminAsync(RegisterAdminInputModel registerAdminInputModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        var result = await userService.RegisterAdminAsync(registerAdminInputModel, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            if (result.ErrorType is RegisterAdminErrorType.CouldNotFindApplicationUser)
            {
                return NotFound(result);
            }
            else if (result.ErrorType is RegisterAdminErrorType.EmailAlreadyExists)
            {
                return BadRequest(result);
            }
            else if (result.ErrorType is RegisterAdminErrorType.CouldNotCreateApplicationUser or RegisterAdminErrorType.CouldNotCreateAdmin)
            {
                return UnprocessableEntity(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }

    [HttpPost("RegisterEmployee"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResponseDTO<Admin>>> RegisterEmployeeAsync(RegisterEmployeeInputModel registerEmployeeInputModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        var result = await userService.RegisterEmployeeAsync(registerEmployeeInputModel, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            if (result.ErrorType is RegisterEmployeeErrorType.CouldNotFindApplicationUser)
            {
                return NotFound(result);
            }
            else if (result.ErrorType is RegisterEmployeeErrorType.EmailAlreadyExists)
            {
                return BadRequest(result);
            }
            else if (result.ErrorType is RegisterEmployeeErrorType.CouldNotCreateApplicationUser or RegisterEmployeeErrorType.CouldNotCreateEmployee)
            {
                return UnprocessableEntity(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }

    [HttpGet("GetEmployeeList"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResponseDTO<IEnumerable<EmployeeListDTO>>>> GetEmployeeListAsync(CancellationToken cancellationToken)
    {
        var result = await userService.GetEmployeeListAsync(cancellationToken);

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

    [HttpGet("GetAdminList"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResponseDTO<IEnumerable<AdminListDTO>>>> GetAdminListAsync(CancellationToken cancellationToken)
    {
        var result = await userService.GetAdminListAsync(cancellationToken);

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

    [HttpGet("GetBookerList"), Authorize(Roles = "Admin")]
    public async Task<ActionResult<ResponseDTO<IEnumerable<BookerListDTO>>>> GetBookerListAsync(CancellationToken cancellationToken)
    {
        var result = await userService.GetBookerListAsync(cancellationToken);

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