using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record Enable2faCommand(string UserId, string Token) : IRequest;

public class Enable2faCommandValidator : AbstractValidator<Enable2faCommand>
{
    public Enable2faCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}

public class Enable2faCommandHandler(IAuthenticationService authenticationService)
    : IRequestHandler<Enable2faCommand>
{
    public async Task Handle(Enable2faCommand request, CancellationToken cancellationToken)
    {
        await authenticationService.Enable2faAsync(request.UserId, request.Token);
    }
}
