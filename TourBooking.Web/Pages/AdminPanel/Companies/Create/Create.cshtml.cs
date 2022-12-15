using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.AdminPanel.Companies.Create;

[Authorize(Roles = "Admin")]
public class CreateModel : BasePageModel
{
    [BindProperty]
    public RegisterCompanyInputModel RegisterCompanyInputModel { get; set; } = new();

    private readonly ICompanyService companyService;

    public CreateModel(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    public async Task<IActionResult> OnPostRegisterCompanyAsync(RegisterCompanyInputModel registerCompanyInputModel)
    {
        var registerCompanyResult = await companyService.RegisterCompanyAsync(registerCompanyInputModel);

        if (registerCompanyResult.IsSuccess)
        {
            return LocalRedirect($"~/company/{registerCompanyResult.Content!.Handle}");
        }
        else
        {
            DisplayError(registerCompanyResult);
        }

        return Page();
    }
}