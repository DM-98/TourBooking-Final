using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Constants;
using TourBooking.Core.Domain;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Authentication;

public class ConfirmEmailModel : BasePageModel
{
    private readonly IUserService userService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICompanyService companyService;
    private readonly IEmailSender emailSender;

    public ConfirmEmailModel(IUserService userService, UserManager<ApplicationUser> userManager, ICompanyService companyService, IEmailSender emailSender) : base(companyService)
    {
        this.userService = userService;
        this.userManager = userManager;
        this.companyService = companyService;
        this.emailSender = emailSender;
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] string confirmToken, [FromQuery] string appUID, [FromQuery] string localReturnUrl)
    {
        if (string.IsNullOrWhiteSpace(confirmToken) || string.IsNullOrWhiteSpace(appUID) || string.IsNullOrWhiteSpace(localReturnUrl))
        {
            DisplayError("Failed to confirm email - check the link and try again or contact a server administrator.");

            return LocalRedirect(LocalErrorPage);
        }

        var applicationUser = await userManager.FindByIdAsync(appUID);

        if (applicationUser is null)
        {
            DisplayError("Failed to fetch your user information.");

            return LocalRedirect(LocalErrorPage);
        }

        var confirmEmailResult = await userService.ConfirmEmailAsync(Guid.Parse(appUID), confirmToken);

        if (confirmEmailResult.IsSuccess)
        {
            TempData[Globals.AppUID] = appUID;
            TempData[Globals.AuthorizationToken] = await userManager.GenerateUserTokenAsync(applicationUser, "PasswordlessLoginTotpProvider", "passwordless-auth");

            if (IsAuthenticated)
            {
                DisplaySuccess("Booking has now been created.");
            }
            else
            {
                DisplaySuccess($"Booking has now been created. If you want to write messages regarding the booking, you must create a user with the same email ({applicationUser.Email}) that you entered in the booking.");
            }

            var employeesResult = await companyService.GetAllEmployeesEmailListAsync(localReturnUrl[..localReturnUrl.IndexOf("/")]);

            if (employeesResult.IsSuccess)
            {
                var employees = employeesResult.Content!;

                if (employees?.Any() ?? false)
                {
                    foreach (var employee in employees)
                    {
                        if (employee.IsEmailNotificationEnabled)
                        {
                            var bookingURL = BaseURL + "/" + localReturnUrl;
                            var subject = "A new booking has been created";
                            var body = "A new booking has been created in the system, click here to see the booking (remember to be logged in first):" +
                                "<br /> " +
                                "<hr /> " +
                                $"<a href=\"{bookingURL}\" target=\"_blank\">{bookingURL}</a>";

                            await emailSender.SendEmailAsync(employee.Email, subject, body);
                        }
                    }
                }
            }

            return LocalRedirect("~/" + localReturnUrl);
        }
        else
        {
            if (confirmEmailResult.ErrorType is ConfirmEmailErrorType.EmailAlreadyConfirmed)
            {
                TempData[Globals.AppUID] = appUID;
                TempData[Globals.AuthorizationToken] = await userManager.GenerateUserTokenAsync(applicationUser, "PasswordlessLoginTotpProvider", "passwordless-auth");

                return LocalRedirect("~/" + localReturnUrl);
            }

            DisplayError(confirmEmailResult);

            return LocalRedirect(LocalErrorPage);
        }
    }
}