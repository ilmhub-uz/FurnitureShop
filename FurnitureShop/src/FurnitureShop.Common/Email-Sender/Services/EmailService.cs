using FurnitureShop.Common.Email_Sender.Dtos;
using JFA.DependencyInjection;
using MimeKit;

namespace FurnitureShop.Common.Email_Sender.Services;

public class EmailService
{
    private EmailConfiguration _emailConfig;

    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public EmailService(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress(_emailConfig.From, x)));
        Subject = subject;
        Content = content;
    }
}
