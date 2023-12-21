using System.Net;
using System.Net.Mail;
using MVC_PustokPlus.Helpers;
using MVC_PustokPlus.Interfaces;

namespace MVC_PustokPlus.Services;

public class EmailService : IEmailService
{

    IConfiguration _configuration { get; }
    public static bool NeedUpdate { get; set; } = true;
	public static SmtpClient Client { get; private set; }
	public static MailAddress Support { get; private set; }

	public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;

        if (EmailService.NeedUpdate)
        {
            Client = new SmtpClient(_configuration["Email:Host"],
                Convert.ToInt32(_configuration["Email:Port"]));

            DevReport.ConsoleLog(_configuration["Email:Host"]);

            Client.EnableSsl = true;

            Client.Credentials = new NetworkCredential(_configuration["Email:Username"],
                _configuration["Email:Password"]);

            Support = new MailAddress(_configuration["Email:Username"], "Jafar Fun Games");

			EmailService.NeedUpdate = false;
        }
    }

    public bool Send(string mail, string header, string body, bool isHtml)
    {
        MailAddress to = new(mail);
		MailMessage message = new(EmailService.Support, to)
		{
			Body = body,
			Subject = header,
			IsBodyHtml = isHtml
		};

		Client.Send(message);
        return true;
    }
}
