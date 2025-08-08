using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record LoginRequest(string Email, string Password, bool RememberMe) : IRequest<LoginResponse>;

public record LoginResponse(string Token);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid Email.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}

public class LoginRequestHandler(IAuthenticationService authenticationService) : IRequestHandler<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var token = await authenticationService.LoginUser(request.Email, request.Password, request.RememberMe);
        return new LoginResponse(token);
    }
}