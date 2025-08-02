namespace GroovE.Application.Mailing;

public interface IMailService
{
    public Task SendRawMail(string recipient, string rawContent, string htmlContent);
    public Task SendTemplatedMail(string recipient, MailTemplates.EmailTemplate template);
}
