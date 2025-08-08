using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record RegisterRequest(string Email, string Password, string FirstName, string LastName) : IRequest<RegisterResponse>;

public record RegisterResponse(string Token);

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
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

public class RegisterRequestHandler(IAuthenticationService authenticationService) : IRequestHandler<RegisterRequest, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var token = await authenticationService.RegisterUser(request.Email, request.Password, request.FirstName, request.LastName);
        return new RegisterResponse(token);
    }
}
