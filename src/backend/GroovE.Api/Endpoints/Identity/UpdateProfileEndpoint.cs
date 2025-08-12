using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Identity;

public class UpdateProfileEndpoint : IEndpoint
{
    public record UpdateProfileRequest(string FirstName, string LastName);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPut("/identity/profile", Handle)
        .RequireAuthorization()
        .WithSummary("Updates the user's profile")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, [FromBody] UpdateProfileRequest request, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        await mediator.Send(new UpdateProfileCommand(userId, request.FirstName, request.LastName));
    }
}
