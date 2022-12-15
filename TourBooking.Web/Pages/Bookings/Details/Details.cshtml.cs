using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Bookings;

public class DetailsModel : BasePageModel
{
    public BookingDetailsDTO? Booking { get; set; }

    private readonly ICompanyService companyService;
    private readonly IBookingService bookingService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IEmailSender emailSender;

    public DetailsModel(ICompanyService companyService, IBookingService bookingService, UserManager<ApplicationUser> userManager, IEmailSender emailSender) : base(companyService)
    {
        this.companyService = companyService;
        this.bookingService = bookingService;
        this.userManager = userManager;
        this.emailSender = emailSender;
    }

    public async Task<IActionResult> OnGetAsync(string handle, string id)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        if (!IsAuthenticated)
        {
            if (TempData[Globals.AuthorizationToken] is not null && TempData[Globals.AppUID] is not null)
            {
                var applicationUser = await userManager.FindByIdAsync(TempData[Globals.AppUID]!.ToString()!);

                if (applicationUser is null)
                {
                    DisplayError("Invalid URL.");

                    return LocalRedirect(LocalErrorPage);
                }

                var isTokenValid = await userManager.VerifyUserTokenAsync(applicationUser, "PasswordlessLoginTotpProvider", "passwordless-auth", (string)TempData[Globals.AuthorizationToken]!);

                if (!isTokenValid)
                {
                    DisplayError("Invalid URL.");

                    return LocalRedirect(LocalErrorPage);
                }

                TempData[Globals.IsAuthenticatedViaEmail] = true;
            }
            else
            {
                DisplayError("Invalid URL.");

                return LocalRedirect(LocalErrorPage);
            }
        }

        var bookingResult = IsInRoleEmployeeForCurrentCompany || IsInRoleAdmin || TempData[Globals.IsAuthenticatedViaEmail] is not null ? await bookingService.GetBookingDetailsAsync(Guid.Parse(id)) : await bookingService.GetBookingDetailsAsync(Guid.Parse(id), UserIdFromClaim);

        if (bookingResult.IsSuccess)
        {
            Booking = bookingResult.Content!;
        }
        else
        {
            DisplayError(bookingResult);

            return LocalRedirect(LocalErrorPage);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCancelBookingAsync(string handle, string bookingId)
    {
        var toggleBookingResult = await bookingService.ToggleBookingStatusAsync(Guid.Parse(bookingId), BookingStatus.Closed);

        if (toggleBookingResult.IsSuccess)
        {
            var employeesEmailListResult = await companyService.GetAllEmployeesEmailListAsync(handle);

            if (employeesEmailListResult.IsSuccess)
            {
                foreach (var employee in employeesEmailListResult.Content!)
                {
                    if (employee.IsEmailNotificationEnabled)
                    {
                        await emailSender.SendEmailAsync(employee.Email!, $"The booking ({bookingId}) has been cancelled", $"Hello, a booking has recently been closed. <br /><hr />Booking Id: <b>{bookingId}</b>");
                    }
                }
            }

            DisplaySuccess("The booking has now been canceled and closed.");

            return LocalRedirect($"~/" + (IsAuthenticated ? handle + "/" + Globals.PageBookings : handle));
        }
        else
        {
            DisplayError(toggleBookingResult);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostReopenBookingAsync(string bookingId)
    {
        var toggleBookingResult = await bookingService.ToggleBookingStatusAsync(Guid.Parse(bookingId), BookingStatus.Active);

        if (toggleBookingResult.IsSuccess)
        {
            DisplaySuccess($"The booking ({bookingId}) has been reactivated.");

            return RedirectToPage();
        }
        else
        {
            DisplayError(toggleBookingResult);
        }

        return Page();
    }
}