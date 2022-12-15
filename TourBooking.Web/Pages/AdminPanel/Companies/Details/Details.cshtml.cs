using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.AdminPanel.Companies.Details;

[Authorize(Roles = "Admin")]
public class DetailsModel : BasePageModel
{
    public CompanyDetailsDTO? SelectedCompany { get; private set; }

    private readonly ICompanyService companyService;

    public DetailsModel(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    public async Task<IActionResult> OnGetAsync(string handle)
    {
        var companyResult = await companyService.GetCompanyDetailsAsync(handle);

        if (companyResult.IsSuccess)
        {
            SelectedCompany = companyResult.Content!;
        }
        else
        {
            DisplayError(companyResult);
        }

        return Page();
    }
}