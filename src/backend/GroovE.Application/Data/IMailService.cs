namespace GroovE.Application.Data;

public interface IMailService
{
    public Task SendMail(string address, string subject, string body);
}
