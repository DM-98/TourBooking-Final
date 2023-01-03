using Microsoft.AspNetCore.Mvc;

namespace TourBooking.Web.Pages.Authentication;

public class LogoutModel : BasePageModel
{
    public IActionResult OnGet(string handle)
    {
#if DEBUG
        Response.Cookies.Delete("AccessToken", new CookieOptions { Secure = true });
        Response.Cookies.Delete("RefreshToken", new CookieOptions { Secure = true });
        Response.Cookies.Delete("RememberMe", new CookieOptions { Secure = true });
#else
        Response.Cookies.Delete("AccessToken", new CookieOptions { Secure = false });
        Response.Cookies.Delete("RefreshToken", new CookieOptions { Secure = false });
        Response.Cookies.Delete("RememberMe", new CookieOptions { Secure = false });
#endif

        return LocalRedirect("~/" + handle);
    }
}