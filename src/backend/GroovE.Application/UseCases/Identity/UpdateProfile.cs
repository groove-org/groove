using FluentValidation;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record UpdateProfileCommand(string UserId, string FirstName, string LastName) : IRequest;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}

public class UpdateProfileCommandHandler(IAuthenticationService authenticationService)
    : IRequestHandler<UpdateProfileCommand>
{
    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        await authenticationService.UpdateProfileAsync(request.UserId, request.FirstName, request.LastName);
    }
}
