using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Inputs.Nomi4s;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Helpers;
using TourBooking.Core.Interfaces;
using TourBooking.Core.Interfaces.Nomi4s;

namespace TourBooking.Web.Pages.Bookings.Create;

public class CreateModel : BasePageModel
{
    private readonly ICompanyService companyService;
    private readonly IBookingService bookingService;
    private readonly INomi4sBookingService nomi4sBookingService;

    [BindProperty]
    public CreateBookingInputModel CreateBookingInputModel { get; set; } = new CreateBookingInputModel();

    [BindProperty]
    public CreateNomi4sBookingInputModel CreateNomi4sBookingInputModel { get; set; } = new CreateNomi4sBookingInputModel();

    public IEnumerable<CompanyPackageListDTO>? Packages { get; set; }

    public IEnumerable<CompanyMaterialListDTO>? Materials { get; set; }

    public CreateModel(ICompanyService companyService, IBookingService bookingService, INomi4sBookingService nomi4sBookingService) : base(companyService)
    {
        this.companyService = companyService;
        this.bookingService = bookingService;
        this.nomi4sBookingService = nomi4sBookingService;
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

        if (IsAuthenticated)
        {
            CreateBookingInputModel.FirstName = UserNameFromClaim.Split(' ')[0];
            CreateBookingInputModel.LastName = UserNameFromClaim.Split(' ')[1];
            CreateBookingInputModel.Email = UserEmailFromClaim;
            CreateBookingInputModel.PhoneNumber = UserPhoneFromClaim;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreateBookingAsync(string handle, CreateBookingInputModel createBookingInputModel)
    {
        var createBookingResult = await bookingService.CreateBookingAsync(handle, createBookingInputModel, BaseURL + "/confirm");

        if (createBookingResult.IsSuccess)
        {
            if (handle == CompanyHandleList.Nomi4s.EnumToString())
            {
                CreateNomi4sBookingInputModel.BookingId = createBookingResult.Content!.Id;
                await CreateNomi4sBookingAsync(CreateNomi4sBookingInputModel);
            }

            DisplaySuccess("Booking created!");

            return LocalRedirect($"~/{handle}/{Globals.PageBookings}");
        }
        else
        {
            if (createBookingResult.ErrorType is CreateBookingErrorType.EmailNotConfirmed)
            {
                if (handle == CompanyHandleList.Nomi4s.EnumToString())
                {
                    CreateNomi4sBookingInputModel.BookingId = createBookingResult.Content!.Id;
                    await CreateNomi4sBookingAsync(CreateNomi4sBookingInputModel);
                }

                DisplayInfo(createBookingResult.ErrorMessage!);
            }
            else
            {
                DisplayError(createBookingResult);
            }
        }

        return LocalRedirect("~/" + handle);
    }

    public async Task<bool> CreateNomi4sBookingAsync(CreateNomi4sBookingInputModel createNomi4sBookingInputModel)
    {
        var createNomi4sBookingResult = await nomi4sBookingService.CreateNomi4sBookingAsync(createNomi4sBookingInputModel);

        if (createNomi4sBookingResult.IsSuccess)
        {
            return true;
        }

        return false;
    }
}