using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.Helpers;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Authentication;

public class LoginModel : BasePageModel
{
    private readonly IAuthService authService;
    private readonly IConfiguration configuration;
    private readonly IDataProtector dataProtector;

    [BindProperty]
    public LoginInputModel LoginInputModel { get; set; } = new();

    public LoginModel(IAuthService authService, IConfiguration configuration, IDataProtectionProvider dataProtectionProvider, ICompanyService companyService) : base(companyService)
    {
        this.authService = authService;
        this.configuration = configuration;
        dataProtector = dataProtectionProvider.CreateProtector("ProtectCookieValues");
    }

    public async Task<IActionResult> OnGetAsync(string handle, string? returnUrl = default)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            TempData[Globals.ReturnUrl] = returnUrl.StartsWith("/") ? returnUrl?[1..] : returnUrl;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostLoginAsync(string handle, LoginInputModel loginInputModel, string? returnUrl = default)
    {
        var loginResult = await authService.LoginAsync(loginInputModel);

        if (loginResult.IsSuccess)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = loginInputModel.RememberMe ? DateTimeOffset.UtcNow.AddDays(Convert.ToDouble(configuration["JWT:RefreshTokenExpiryInDays"])) : null,
            };

            HttpContext.Response.Cookies.Append("RememberMe", dataProtector.Protect(loginInputModel.RememberMe ? "true" : "false"), cookieOptions);
            HttpContext.Response.Cookies.Append("AccessToken", dataProtector.Protect(loginResult.Content!.AccessToken), cookieOptions);
            HttpContext.Response.Cookies.Append("RefreshToken", dataProtector.Protect(loginResult.Content.RefreshToken), cookieOptions);

            return LocalRedirect(returnUrl.FormatReturnUrl(handle));
        }
        else
        {
            DisplayError(loginResult);
        }

        return RedirectToPage();
    }
}