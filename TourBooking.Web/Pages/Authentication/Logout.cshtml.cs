using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Authentication;

public class LogoutModel : BasePageModel
{
    public LogoutModel(ICompanyService companyService) : base(companyService)
    {
    }

    public async Task<IActionResult> OnGetAsync(string handle)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        return Page();
    }

    public IActionResult OnGetLogout(string handle)
    {
        Response.Cookies.Delete("AccessToken", new CookieOptions { Secure = true });
        Response.Cookies.Delete("RefreshToken", new CookieOptions { Secure = true });
        Response.Cookies.Delete("RememberMe", new CookieOptions { Secure = true });

        return LocalRedirect("~/" + handle);
    }
}