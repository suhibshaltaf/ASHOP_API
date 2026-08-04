using System.Net;
using System.Net.Mail;

try
{
    using var client = new SmtpClient("smtp.gmail.com", 587)
    {
        EnableSsl = true,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(
        "animalfarm.management@gmail.com",
                    "vpnm ehht ogmg bfkz"
        )
    };

    using var mail = new MailMessage(
        "animalfarm.management@gmail.com",
        "sohaibshaltafaltrefe1@gmail.com",
        "Test Email",
        "Hello from Console App");

    Console.WriteLine("Before Send");

    client.Send(mail);

    Console.WriteLine("After Send");
    Console.WriteLine("Email sent successfully.");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();