using FurnitureShop.Common.Email_Sender.Dtos;
using JFA.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace FurnitureShop.Common.Email_Sender.Services;
[Scoped]
public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;
    public EmailSender(IOptions<EmailConfiguration> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }
    public void SendEmail(EmailService emailService)
    {
        var emailMessage = CreateEmailMessage(emailService);
        Send(emailMessage);
    }
    private MimeMessage CreateEmailMessage(EmailService emailService)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
        emailMessage.To.AddRange(emailService.To);
        emailMessage.Subject = emailService.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailService.Content };
        return emailMessage;
    }
    private void Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
