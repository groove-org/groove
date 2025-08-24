
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ConfirmEmailCommand(string UserId, string Code) : IRequest;

public class ConfirmEmailCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<ConfirmEmailCommand>
{
    public Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken) => authenticationService.ConfirmEmailAsync(request.UserId, request.Code);
}
