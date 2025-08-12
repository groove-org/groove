using FluentValidation;
using GroovE.Application.Mailing;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ForgotPasswordCommand(string Email) : IRequest;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}

public class ForgotPasswordCommandHandler(IAuthenticationService authenticationService, IMailService mailService)
    : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var tokenDto = await authenticationService.GeneratePasswordResetTokenAsync(request.Email);

        if (tokenDto is not null)
        {
            var resetLink = $"/reset-password?token={tokenDto.Token}&email={tokenDto.Email}";
            await mailService.SendTemplatedMail(tokenDto.Email, new MailTemplates.ResetPasswordTemplate(tokenDto.Email, tokenDto.FirstName, resetLink));
        }
    }
}
