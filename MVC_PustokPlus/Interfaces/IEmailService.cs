using System.Net.Mail;

namespace MVC_PustokPlus.Interfaces;

public interface IEmailService
{
	public bool Send(string mail, string header, string body, bool isHtml);
}
