using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record GetProfileQuery(string UserId) : IRequest<UserProfileDto>;

public class GetProfileQueryHandler(IAuthenticationService authenticationService)
    : IRequestHandler<GetProfileQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await authenticationService.GetProfileAsync(request.UserId);
    }
}
