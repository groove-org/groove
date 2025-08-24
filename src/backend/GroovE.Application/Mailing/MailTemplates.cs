namespace GroovE.Application.Mailing;

public static class MailTemplates
{
    public abstract record EmailTemplate;

    public record VerifyEmailTemplate(string FirstName, string ConfirmationLink) : EmailTemplate;
    public record ResetPasswordTemplate(string FirstName, string ResetLink) : EmailTemplate;
}