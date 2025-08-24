using FluentValidation;
using GroovE.Application.Mailing;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record RegisterCommand(string Email, string Password, string FirstName, string LastName) : IRequest<RegisterResult>;

public record RegisterResult(string Token);

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid Email.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
    }
}

public class RegisterCommandHandler(IIdentityService authenticationService, IMailService mailService) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var token = await authenticationService.RegisterUserAsync(request.Email, request.Password, request.FirstName, request.LastName);

        var link = await authenticationService.GenerateEmailConfirmationLinkAsync(request.Email);
        var userId = await authenticationService.GetUserIdByEmail(request.Email);
        var user = await authenticationService.GetProfileAsync(userId);

        await mailService.SendTemplatedMail(user.Email, new MailTemplates.VerifyEmailTemplate(user.FirstName, link));

        return new RegisterResult(token);
    }
}
