using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Bookings.Details;

[Authorize]
public class NewMessageModel : BasePageModel
{
    private readonly IBookingService bookingService;
    private readonly IEmailSender emailSender;
    private readonly UserManager<ApplicationUser> userManager;

    [BindProperty]
    public CreateMessageInputModel CreateMessageInputModel { get; set; } = new();

    public string? BookingId { get; set; }

    public NewMessageModel(ICompanyService companyService, IBookingService bookingService, IEmailSender emailSender, UserManager<ApplicationUser> userManager) : base(companyService)
    {
        this.bookingService = bookingService;
        this.emailSender = emailSender;
        this.userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string handle, string bookingId)
    {
        if (!await LoadCompanyThemeAsync(handle))
        {
            return LocalRedirect(LocalErrorPage);
        }

        BookingId = bookingId;

        return Page();
    }

    public async Task<IActionResult> OnPostCreateNewMessageAsync(string handle, string bookingId, CreateMessageInputModel createMessageInputModel)
    {
        if (string.IsNullOrWhiteSpace(bookingId) || string.IsNullOrWhiteSpace(handle))
        {
            DisplayError("Failed to load this page.");

            return LocalRedirect(LocalErrorPage);
        }

        createMessageInputModel.BookingId = Guid.Parse(bookingId);
        createMessageInputModel.ApplicationUserId = UserIdFromClaim;

        var createMessageResult = await bookingService.CreateMessageAsync(createMessageInputModel);

        if (createMessageResult.IsSuccess)
        {
            if (IsInRoleEmployee || IsInRoleAdmin)
            {
                var bookingDetailsResult = await bookingService.GetBookingDetailsAsync(Guid.Parse(bookingId));

                if (bookingDetailsResult.IsSuccess)
                {
                    var applicationUser = await userManager.FindByEmailAsync(bookingDetailsResult.Content!.BookersEmail);

                    if (applicationUser is null)
                    {
                        DisplayWarning("Message sent, but failed to fetch booker, and unable to notify via email.");

                        return LocalRedirect($"~/{handle}/booking/{bookingId}");
                    }

                    var confirmURL = BaseURL + "/confirm";
                    var confirmToken = await userManager.GenerateUserTokenAsync(applicationUser, "PasswordlessLoginTotpProvider", "passwordless-auth");
                    var localReturnUrl = handle + "/booking/" + bookingId;
                    var link = confirmURL + "?confirmToken=" + confirmToken + "&appUID=" + applicationUser.Id + "&localReturnUrl=" + localReturnUrl;

                    var subject = "New message regarding your booking";
                    var body = $"Hello {applicationUser.UserName}, you've gotten a new message regarding your booking. <br />" +
                        "Read the message by following this link: <br /> <hr />" +
                        $"<a href=\"{link}\" target=\"_blank\">{link}</a>" +
                        "<br /><br />If you wish to respond to the message, you must register a new user with the same email as in the booking.";

                    await emailSender.SendEmailAsync(applicationUser.Email!, subject, body);
                }
            }

            DisplaySuccess("Message sent.");

            return LocalRedirect($"~/{handle}/booking/{bookingId}");
        }
        else
        {
            DisplayError(createMessageResult);
        }

        return Page();
    }
}