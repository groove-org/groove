namespace GroovE.Application.Mailing;

public static class MailTemplates
{
    public abstract record EmailTemplate(string Email);

    public record VerifyEmailTemplate(string Email, string FirstName, string ConfirmationLink) : EmailTemplate(Email);
}