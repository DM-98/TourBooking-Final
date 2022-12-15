using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages;

public class IndexModel : BasePageModel
{
    private readonly ICompanyService companyService;

    public IndexModel(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var firstCompanyResult = await companyService.GetAllAsync();

        if (firstCompanyResult.IsSuccess)
        {
            return LocalRedirect("~/" + firstCompanyResult.Content!.FirstOrDefault()?.Handle ?? string.Empty);
        }

        return Page();
    }
}