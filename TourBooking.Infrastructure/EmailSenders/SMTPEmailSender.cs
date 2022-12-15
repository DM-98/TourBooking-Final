using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.EmailSenders;

public sealed class SMTPEmailSender : IEmailSender
{
    private readonly string host;
    private readonly int port;
    private readonly string username;
    private readonly string password;

    public SMTPEmailSender(IConfiguration configuration)
    {
        host = configuration["SMTP:Host"] ?? throw new ArgumentNullException(nameof(host), "SMTP:Host could not be found in appsettings.");
        port = int.Parse(configuration["SMTP:Port"] ?? throw new ArgumentNullException(nameof(port), "SMTP:Port could not be found in appsettings."));
        username = configuration["SMTP:Username"] ?? throw new ArgumentNullException(nameof(username), "SMTP:Username could not be found in appsettings.");
        password = configuration["SMTP:Password"] ?? throw new ArgumentNullException(nameof(password), "SMTP:Password could not be found in appsettings.");
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(username));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        try
        {
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}