using MediatR;

namespace GroovE.Application.UseCases.Auth;

public record ConfirmEmailRequest(string UserId, string Code) : IRequest;

public class ConfirmEmailRequestHandler(IAuthenticationService authenticationService) : IRequestHandler<ConfirmEmailRequest>
{
    public async Task Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        await authenticationService.ConfirmEmailAsync(request.UserId, request.Code);
    }
}
