using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TourBooking.Core.Constants;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages;

public abstract class BasePageModel : PageModel
{
    public string BaseURL => $"{Request.Scheme}://{Request.Host}";

    public Guid UserIdFromClaim => Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

    public string UserNameFromClaim => User.Claims.First(x => x.Type == ClaimTypes.Name).Value;

    public string UserEmailFromClaim => User.Claims.First(x => x.Type == ClaimTypes.Email).Value;

    public string UserPhoneFromClaim => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value ?? string.Empty;

    public bool IsAuthenticated => User.Identity is not null && User.Identity.IsAuthenticated;

    public bool IsInRoleAdmin => User.IsInRole(RoleType.Admin.ToString());

    public bool IsInRoleEmployee => User.IsInRole(RoleType.Employee.ToString());

    public bool IsInRoleEmployeeForCurrentCompany => User.IsInRole(Company?.Handle ?? string.Empty);

    public bool IsInRoleBooker => User.IsInRole(RoleType.Booker.ToString());

    protected string LocalErrorPage => "~/error";

    protected string LocalCompanyUnauthorizedPage => $"~/{Company!.Handle}/uautoriseret?returnUrl=" + Request.GetEncodedPathAndQuery();

    public CompanyThemeDTO? Company { get; private set; }

    private readonly ICompanyService? companyService;

    public BasePageModel(ICompanyService? companyService = null)
    {
        this.companyService = companyService;
    }

    protected async Task<bool> LoadCompanyThemeAsync(string handle)
    {
        if (companyService is null)
        {
#if DEBUG
            throw new ArgumentNullException(nameof(companyService), "Husk at parse companyService i base constructor for at bruge LoadCompanyTheme-metoden.");
#else
            throw new ArgumentNullException(nameof(companyService), "Serverfejl: Fejl ved indlæsning af virksomhed - kontakt en server administrator.");
#endif
        }

        var companyResult = await companyService.LoadCompanyThemeAsync(handle);

        if (companyResult.IsSuccess)
        {
            Company = companyResult.Content!;

            ViewData[Globals.CompanyId] = companyResult.Content!.Id;
            ViewData[Globals.CompanyName] = companyResult.Content!.Name;
            ViewData[Globals.CompanyHandle] = companyResult.Content!.Handle;
            ViewData[Globals.CompanyWebsite] = companyResult.Content!.Website;
            ViewData[Globals.CompanyLogoUrl] = companyResult.Content!.LogoUrl;
            ViewData[Globals.CompanyNavigationBackgroundColor] = companyResult.Content!.NavigationBackgroundColor;
            ViewData[Globals.CompanyNavigationButtonTextColor] = companyResult.Content!.NavigationButtonTextColor;
            ViewData[Globals.CompanyBackgroundColor] = companyResult.Content!.BackgroundColor;
            ViewData[Globals.CompanyContainerColor] = companyResult.Content!.ContainerColor;
            ViewData[Globals.CompanyTextColor] = companyResult.Content!.TextColor;
            ViewData[Globals.CompanyButtonColor] = companyResult.Content!.ButtonColor;
            ViewData[Globals.CompanyButtonTextColor] = companyResult.Content!.ButtonTextColor;

            return true;
        }
        else
        {
            DisplayError(companyResult);

            return false;
        }
    }

    protected void DisplayError<T>(ResponseDTO<T> responseDTO) where T : class
    {
#if DEBUG
        TempData["ErrorMessage"] = responseDTO.ErrorMessage;
        TempData["ErrorMessage"] += responseDTO.ExceptionMessage?.Any() ?? false ? " | " + responseDTO.ExceptionMessage : string.Empty;
        TempData["ErrorMessage"] += responseDTO.StackTrace?.Any() ?? false ? " | " + responseDTO.StackTrace : string.Empty;
#else
		TempData["ErrorMessage"] = responseDTO.ErrorMessage;
#endif
    }

    protected void DisplayError(string message) => TempData["ErrorMessage"] = message;

    protected void DisplayInfo(string message) => TempData["InfoMessage"] = message;

    protected void DisplayWarning(string message) => TempData["WarningMessage"] = message;

    protected void DisplaySuccess(string message) => TempData["SuccessMessage"] = message;
}