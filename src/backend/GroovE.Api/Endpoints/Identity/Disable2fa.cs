using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Identity;

public class Disable2fa : IEndpoint
{
    public static RouteHandlerBuilder Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/2fa/disable", Handle)
        .RequireAuthorization()
        .WithSummary("Disables 2FA for the user")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        await mediator.Send(new Disable2faCommand(userId));
    }
}
