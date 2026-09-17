using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ASHOP.BLL.Common
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Console.WriteLine("1 - Creating email message");

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("ASHOP", _config["EmailSetting:Email"]));
                emailMessage.To.Add(MailboxAddress.Parse(email));
                emailMessage.Subject = subject;

                emailMessage.Body = new BodyBuilder
                {
                    HtmlBody = message
                }.ToMessageBody();

                using var smtp = new SmtpClient();

                Console.WriteLine("2 - Connecting to Gmail...");

                await smtp.ConnectAsync(
                    _config["EmailSetting:Host"],
                    int.Parse(_config["EmailSetting:Port"]),
                    SecureSocketOptions.SslOnConnect);

                Console.WriteLine("3 - Connected successfully");

                Console.WriteLine("4 - Authenticating...");

                await smtp.AuthenticateAsync(
                    _config["EmailSetting:Email"],
                    _config["EmailSetting:Password"]
                );

                Console.WriteLine("5 - Authentication successful");

                Console.WriteLine("6 - Sending email...");

                await smtp.SendAsync(emailMessage);

                Console.WriteLine("7 - Email sent successfully");

                await smtp.DisconnectAsync(true);

                Console.WriteLine("8 - Disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}