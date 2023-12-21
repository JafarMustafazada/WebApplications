using System.Net;
using System.Net.Mail;

namespace MVC_PustokPlus.Helpers;

public class EmailService
{
	public bool NeedUpdate { get; set; } = true;
    IConfiguration _configuration {  get; }
    public static SmtpClient Client { get; private set; }
	public static MailAddress Support {  get; private set; }

    public EmailService(IConfiguration configuration)
	{
		this._configuration = configuration;

		if (this.NeedUpdate)
		{
			EmailService.Client = new SmtpClient(this._configuration["Email:Host"], 
				Convert.ToInt32(this._configuration["Email:Port"]));

			DevReport.ConsoleLog(this._configuration["Email:Host"]);

			EmailService.Client.EnableSsl = true;

			EmailService.Client.Credentials = new NetworkCredential(this._configuration["Email:Username"],
				this._configuration["Email:Password"]);

			EmailService.Support = new MailAddress(this._configuration["Email:Username"], "Jafar Fun Games");

			this.NeedUpdate = false;
		}
	}

	public bool Send(string mail, string header, string body, bool isHtml)
	{
		MailAddress to = new MailAddress(mail);
		MailMessage message = new MailMessage(EmailService.Support, to);
		message.Body = body;
		message.Subject = header;
		message.IsBodyHtml = isHtml;

		EmailService.Client.Send(message);
		return true;
	}
}
