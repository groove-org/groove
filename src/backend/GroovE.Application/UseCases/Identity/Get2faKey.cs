using GroovE.Application.UseCases.Identity.Dtos;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record Get2faKeyQuery(string UserId) : IRequest<TwoFactorAuthenticationDto>;

public class Get2faKeyQueryHandler(IIdentityService authenticationService)
    : IRequestHandler<Get2faKeyQuery, TwoFactorAuthenticationDto>
{
    public async Task<TwoFactorAuthenticationDto> Handle(Get2faKeyQuery request, CancellationToken cancellationToken)
    {
        return await authenticationService.Generate2faKeyAsync(request.UserId);
    }
}
