using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.AdminPanel.Users.Create;

[Authorize(Roles = "Admin")]
public class CreateModel : BasePageModel
{
    public RoleType? SelectedRoleType { get; set; }

    public IEnumerable<Company>? Companies { get; set; }

    [BindProperty]
    public RegisterAdminInputModel? RegisterAdminInputModel { get; set; }

    [BindProperty]
    public RegisterEmployeeInputModel? RegisterEmployeeInputModel { get; set; }

    private readonly IUserService userService;
    private readonly ICompanyService companyService;

    public CreateModel(IUserService userService, ICompanyService companyService)
    {
        this.userService = userService;
        this.companyService = companyService;
    }

    public async Task<IActionResult> OnGetAsync(string roleType)
    {
        if (roleType.Equals(RoleType.Employee.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            SelectedRoleType = RoleType.Employee;
            RegisterEmployeeInputModel = new RegisterEmployeeInputModel();

            var companiesResult = await companyService.GetAllAsync();

            if (companiesResult.IsSuccess)
            {
                Companies = companiesResult.Content!;
            }
            else
            {
                DisplayWarning("Register at least one company before attempting to create an employee.");
            }
        }
        else if (roleType.Equals(RoleType.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            SelectedRoleType = RoleType.Admin;
            RegisterAdminInputModel = new RegisterAdminInputModel();
        }
        else
        {
            DisplayError("Invalid user type.");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostRegisterAdminAsync(RegisterAdminInputModel registerAdminInputModel)
    {
        var registerAdminResult = await userService.RegisterAdminAsync(registerAdminInputModel);

        if (registerAdminResult.IsSuccess)
        {
            return LocalRedirect($"~/admin/{registerAdminResult.Content!.Id}");
        }
        else
        {
            DisplayError(registerAdminResult);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostRegisterEmployeeAsync(RegisterEmployeeInputModel registerEmployeeInputModel)
    {
        var registerEmployeeResult = await userService.RegisterEmployeeAsync(registerEmployeeInputModel);

        if (registerEmployeeResult.IsSuccess)
        {
            return LocalRedirect($"~/employee/{registerEmployeeResult.Content!.Id}");
        }
        else
        {
            DisplayError(registerEmployeeResult);
        }

        return Page();
    }
}