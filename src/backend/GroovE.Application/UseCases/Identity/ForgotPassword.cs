using FluentValidation;
using GroovE.Application.Mailing;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ForgotPasswordCommand(string Email) : IRequest;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator() => RuleFor(x => x.Email).NotEmpty().EmailAddress();
}

public class ForgotPasswordCommandHandler(IIdentityService authenticationService, IMailService mailService)
    : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = await authenticationService.GeneratePasswordResetTokenAsync(request.Email);
        var userId = await authenticationService.GetUserIdByEmail(request.Email);
        var user = await authenticationService.GetProfileAsync(userId);

        var resetLink = $"/reset-password?token={token}&email={request.Email}";
        await mailService.SendTemplatedMail(request.Email, new MailTemplates.ResetPasswordTemplate(user.FirstName, resetLink));
    }
}
