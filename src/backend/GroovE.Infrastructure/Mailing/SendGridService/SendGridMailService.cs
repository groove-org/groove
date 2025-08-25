using GroovE.Application.Mailing;

namespace GroovE.Infrastructure.Mailing.SendGridService;

internal class SendGridMailService : IMailService
{
    public Task SendRawMail(string recipient, string rawContent, string htmlContent) => throw new NotImplementedException();
    public Task SendTemplatedMail(string recipient, MailTemplates.EmailTemplate template) => throw new NotImplementedException();
}
