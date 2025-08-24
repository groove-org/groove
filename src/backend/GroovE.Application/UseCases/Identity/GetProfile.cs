using GroovE.Application.Common;
using MediatR;

namespace GroovE.Application.UseCases.Identity;

public record GetProfileQuery(string UserId) : IRequest<UserProfileDto>;

public class GetProfileQueryHandler(ICurrentUser currentUser)
    : IRequestHandler<GetProfileQuery, UserProfileDto>
{
    public Task<UserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new UserProfileDto
        (
            currentUser.Id,
            currentUser.FirstName,
            currentUser.LastName,
            currentUser.Email
        ));
    }
}
