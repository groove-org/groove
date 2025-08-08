using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record ConfirmEmailRequest(string UserId, string Code) : IRequest;

public class ConfirmEmailRequestHandler(IAuthenticationService authenticationService) : IRequestHandler<ConfirmEmailRequest>
{
    public Task Handle(ConfirmEmailRequest request, CancellationToken cancellationToken) => authenticationService.ConfirmEmailAsync(request.UserId, request.Code);
}
