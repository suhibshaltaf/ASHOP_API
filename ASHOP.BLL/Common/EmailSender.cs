using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ASHOP.BLL.Common
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Console.WriteLine("1 - Creating email message");

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("ASHOP", "animalfarm.management@gmail.com"));
                emailMessage.To.Add(MailboxAddress.Parse(email));
                emailMessage.Subject = subject;

                emailMessage.Body = new BodyBuilder
                {
                    HtmlBody = message
                }.ToMessageBody();

                using var smtp = new SmtpClient();

                Console.WriteLine("2 - Connecting to Gmail...");

                await smtp.ConnectAsync(
                    "smtp.gmail.com",
                    465,
                    SecureSocketOptions.SslOnConnect);

                Console.WriteLine("3 - Connected successfully");

                Console.WriteLine("4 - Authenticating...");

                await smtp.AuthenticateAsync(
                    "animalfarm.management@gmail.com",
                    "qaxd obsn sqfb mpio"
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