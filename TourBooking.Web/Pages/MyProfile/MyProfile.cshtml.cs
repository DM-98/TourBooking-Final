using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.MyProfile;

[Authorize]
public class MyProfileModel : BasePageModel
{
    public MyProfileModel(ICompanyService companyService) : base(companyService)
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
}