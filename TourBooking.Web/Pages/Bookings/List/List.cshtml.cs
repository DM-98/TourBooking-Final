using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Bookings.List;

[Authorize]
public class ListModel : BasePageModel
{
    private readonly IBookingService bookingService;

    public IEnumerable<BookingListDTO>? Bookings { get; set; }

    public ListModel(ICompanyService companyService, IBookingService bookingService) : base(companyService)
    {
        this.bookingService = bookingService;
    }

    public async Task<IActionResult> OnGetAsync(string handle)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        var bookingsResult = IsInRoleEmployeeForCurrentCompany || IsInRoleAdmin ? await bookingService.GetBookingListAsync(handle) : await bookingService.GetBookingListAsync(handle, UserIdFromClaim);

        if (bookingsResult.IsSuccess)
        {
            Bookings = bookingsResult.Content!;
        }
        else
        {
            DisplayError(bookingsResult);
        }

        return Page();
    }
}