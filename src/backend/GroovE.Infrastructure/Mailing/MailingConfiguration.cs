using GroovE.Infrastructure.Mailing.SendGridService;

namespace GroovE.Infrastructure.Mailing;

internal class MailingConfiguration
{
    public string From { get; set; } = string.Empty;

    public MailService Service { get; set; }

    public SendGridConfiguration SendGridConfiguration { get; set; } = new();
}

internal enum MailService
{
    Logger,
    SendGrid
}
