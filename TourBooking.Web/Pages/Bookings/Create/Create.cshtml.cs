using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Bookings.Create;

public class CreateModel : BasePageModel
{
    private readonly ICompanyService companyService;
    private readonly IBookingService bookingService;

    [BindProperty]
    public CreateBookingInputModel CreateBookingInputModel { get; set; } = new CreateBookingInputModel();

    public IEnumerable<CompanyPackageListDTO>? Packages { get; set; }

    public IEnumerable<CompanyMaterialListDTO>? Materials { get; set; }

    public CreateModel(ICompanyService companyService, IBookingService bookingService) : base(companyService)
    {
        this.companyService = companyService;
        this.bookingService = bookingService;
    }

    public async Task<IActionResult> OnGetAsync(string handle)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        var packageListResult = await companyService.GetCompanyPackageListAsync(handle);

        if (packageListResult.IsSuccess)
        {
            if (!packageListResult.Content!.Any())
            {
                DisplayWarning("This company has no packages set up yet to allow bookings. Contact the company for more info.");

                return LocalRedirect("~/" + handle);
            }

            Packages = packageListResult.Content!;
        }
        else
        {
            DisplayError(packageListResult);
        }

        var materialListResult = await companyService.GetCompanyMaterialListAsync(handle);

        if (materialListResult.IsSuccess)
        {
            Materials = materialListResult.Content!;
        }
        else
        {
            DisplayError(materialListResult);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreateBookingAsync(string handle, CreateBookingInputModel createBookingInputModel)
    {
        var createBookingResult = await bookingService.CreateBookingAsync(handle, createBookingInputModel, BaseURL + "/confirm");

        if (createBookingResult.IsSuccess)
        {
            DisplaySuccess("Booking created!");

            return LocalRedirect($"~/{handle}/{Globals.PageBookings}");
        }
        else
        {
            if (createBookingResult.ErrorType is CreateBookingErrorType.EmailNotConfirmed)
            {
                DisplayInfo(createBookingResult.ErrorMessage!);
            }
            else
            {
                DisplayError(createBookingResult);
            }
        }

        return Page();
    }
}