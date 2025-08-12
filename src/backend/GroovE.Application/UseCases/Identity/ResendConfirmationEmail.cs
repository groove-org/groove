using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ResendConfirmationEmailCommand(string Email) : IRequest;

public class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommand>
{
    public ResendConfirmationEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}

public class ResendConfirmationEmailCommandHandler(IAuthenticationService authenticationService)
    : IRequestHandler<ResendConfirmationEmailCommand>
{
    public async Task Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        await authenticationService.ResendConfirmationEmailAsync(request.Email);
    }
}
