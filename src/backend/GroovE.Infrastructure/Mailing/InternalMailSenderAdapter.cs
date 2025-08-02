using GroovE.Application.Mailing;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace GroovE.Infrastructure.Mailing;

internal class InternalMailSenderAdapter(IMailService mailService) : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        => mailService.SendTemplatedMail(email, new MailTemplates.VerifyEmailTemplate(email, user.UserName ?? "User", confirmationLink));

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode) => throw new NotImplementedException();
    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink) => throw new NotImplementedException();
}
