using FluentValidation;
using GroovE.Application.Mailing;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ResendConfirmationEmailCommand(string Email) : IRequest;

public class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommand>
{
    public ResendConfirmationEmailCommandValidator() => RuleFor(x => x.Email).NotEmpty().EmailAddress();
}

public class ResendConfirmationEmailCommandHandler(IIdentityService authenticationService, IMailService mailService)
    : IRequestHandler<ResendConfirmationEmailCommand>
{
    public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var link = await authenticationService.GenerateEmailConfirmationLinkAsync(request.Email);
        var userId = await authenticationService.GetUserIdByEmail(request.Email);
        var user = await authenticationService.GetProfileAsync(userId);

        await mailService.SendTemplatedMail(user.Email, new MailTemplates.VerifyEmailTemplate(user.FirstName, link));
    }
}