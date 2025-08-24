using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record LoginWith2faCommand(string TwoFactorToken, string TwoFactorCode, bool RememberMe) : IRequest<LoginWith2faResponse>;

public record LoginWith2faResponse(string Token);

public class LoginWith2faCommandValidator : AbstractValidator<LoginWith2faCommand>
{
    public LoginWith2faCommandValidator() => RuleFor(x => x.TwoFactorCode).NotEmpty();
}

public class LoginWith2faCommandHandler(IIdentityService authenticationService)
    : IRequestHandler<LoginWith2faCommand, LoginWith2faResponse>
{
    public async Task<LoginWith2faResponse> Handle(LoginWith2faCommand request, CancellationToken cancellationToken)
    {
        var token = await authenticationService.LoginWith2faAsync(request.TwoFactorToken, request.TwoFactorCode, request.RememberMe);
        return new LoginWith2faResponse(token);
    }
}
