using GroovE.Application.Mailing;
using Microsoft.Extensions.Logging;

namespace GroovE.Infrastructure.Mailing.LoggerService;

internal class LoggerMailService(ILogger<LoggerMailService> logger) : IMailService
{
    public Task SendRawMail(string recipient, string rawContent, string htmlContent)
    {
        logger.LogInformation("Sending mail with raw content {RawContent} and HTML content {HtmlContent} to recipient {Recipient}", rawContent, htmlContent, recipient);

        return Task.CompletedTask;
    }

    public Task SendTemplatedMail(string recipient, MailTemplates.EmailTemplate template)
    {
        logger.LogInformation("Sending mail with template {Template} to recipient {Recipient}", template, recipient);

        return Task.CompletedTask;
    }
}
