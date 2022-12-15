using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Route("api/booking")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService bookingService;

    public BookingController(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    [HttpPost("CreateBooking"), AllowAnonymous]
    public async Task<ActionResult<ResponseDTO<Booking>>> CreateBookingAsync(string handle, CreateBookingInputModel createBookingInputModel, string confirmUrl, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem();
        }

        var result = await bookingService.CreateBookingAsync(handle, createBookingInputModel, confirmUrl, cancellationToken);

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
            else if (result.ErrorType is CreateBookingErrorType.CouldNotFindApplicationUser)
            {
                return NotFound(result);
            }
            else if (result.ErrorType is CreateBookingErrorType.InvalidUserName)
            {
                return BadRequest(result);
            }
            else if (result.ErrorType is CreateBookingErrorType.CouldNotCreateApplicationUser or CreateBookingErrorType.CouldNotCreateBookerExistingUser or CreateBookingErrorType.CouldNotCreateBookerNewUser or CreateBookingErrorType.CouldNotCreateBooking or CreateBookingErrorType.CouldNotUpdateMaterial)
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