using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Identity;

public class Enable2fa : IEndpoint
{
    public record Enable2faRequest(string Token);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/2fa/enable", Handle)
        .RequireAuthorization()
        .WithSummary("Enables 2FA for the user")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, [FromBody] Enable2faRequest request, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        await mediator.Send(new Enable2faCommand(userId, request.Token));
    }
}
