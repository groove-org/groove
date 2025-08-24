using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record Disable2faCommand(string UserId) : IRequest;

public class Disable2faCommandValidator : AbstractValidator<Disable2faCommand>
{
    public Disable2faCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

public class Disable2faCommandHandler(IIdentityService authenticationService)
    : IRequestHandler<Disable2faCommand>
{
    public async Task Handle(Disable2faCommand request, CancellationToken cancellationToken)
    {
        await authenticationService.Disable2faAsync(request.UserId);
    }
}
