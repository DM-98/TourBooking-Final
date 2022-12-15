using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.CompanySettings;

[Authorize(Roles = "Employee, Admin")]
public class CompanySettingsModel : BasePageModel
{
    public CompanySettingsModel(ICompanyService companyService) : base(companyService)
    {
    }

    public async Task<IActionResult> OnGetAsync(string handle)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        if (!IsInRoleEmployeeForCurrentCompany && !IsInRoleAdmin)
        {
            return LocalRedirect(LocalCompanyUnauthorizedPage);
        }

        return Page();
    }
}