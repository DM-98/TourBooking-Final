using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.AdminPanel.Companies.List;

[Authorize(Roles = "Admin")]
public class ListModel : BasePageModel
{
    public IEnumerable<CompanyListDTO>? Companies { get; set; }

    private readonly ICompanyService companyService;

    public ListModel(ICompanyService companyService) : base(companyService)
    {
        this.companyService = companyService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var companiesResult = await companyService.GetCompanyListAsync();

        if (companiesResult.IsSuccess)
        {
            Companies = companiesResult.Content!;
        }
        else
        {
            DisplayError(companiesResult);

            return LocalRedirect(LocalErrorPage);
        }

        return Page();
    }
}