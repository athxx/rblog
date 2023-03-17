using System.Net;
using System.Net.Mail;

namespace Core.Util.Email;

public class EmailClient
{
    private readonly SmtpClient _smtpClient;

    public EmailClient(string smtpServer, int smtpPort, string username, string password)
    {
        _smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };
    }

    public void SendEmail(string from, string to, string subject, string body)
    {
        var mailMessage = new MailMessage(from, to, subject, body);
        _smtpClient.Send(mailMessage);
    }
}