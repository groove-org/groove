
using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record LoginCommand(string Email, string Password, bool RememberMe) : IRequest<LoginResult>;

public record LoginResult(string? Token, bool RequiresTwoFactor);

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid Email.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

public class LoginCommandHandler(IIdentityService authenticationService) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.LoginUserAsync(request.Email, request.Password, request.RememberMe);
        return new LoginResult(response.Token, response.RequiresTwoFactor);
    }
}