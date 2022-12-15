namespace TourBooking.Core.Interfaces;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}