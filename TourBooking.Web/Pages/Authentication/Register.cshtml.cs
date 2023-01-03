using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Authentication;

public class RegisterModel : BasePageModel
{
    [BindProperty]
    public RegisterBookerInputModel RegisterBookerInputModel { get; set; } = null!;

    private readonly IUserService userService;

    public RegisterModel(IUserService userService, ICompanyService companyService) : base(companyService)
    {
        this.userService = userService;
    }

    public async Task<IActionResult> OnGetAsync(string handle, string? returnUrl = default)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        TempData[Globals.ReturnUrl] = returnUrl ?? string.Empty;

        RegisterBookerInputModel = new RegisterBookerInputModel();

        return Page();
    }

    public async Task<IActionResult> OnPostRegisterBookerAsync(string handle, RegisterBookerInputModel registerBookerInputModel)
    {
        var registerBookerResult = await userService.RegisterBookerAsync(registerBookerInputModel);

        if (registerBookerResult.IsSuccess)
        {
            DisplaySuccess("Your user has been created. Login to continue.");

            return LocalRedirect($"~/{handle}/{Globals.PageLogin}" + "?returnUrl=" + (string)TempData[Globals.ReturnUrl]!);
        }
        else
        {
            DisplayError(registerBookerResult);
        }

        return RedirectToPage();
    }
}