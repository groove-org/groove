using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using GroovE.Application.UseCases.Identity.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Identity;

public class Get2faKey : IEndpoint
{
    public static RouteHandlerBuilder Map(IEndpointRouteBuilder app) => app
        .MapGet("/identity/2fa/key", Handle)
        .RequireAuthorization()
        .WithSummary("Gets a 2FA key for the user")
        .WithTags("Identity");

    public static async Task<TwoFactorAuthenticationDto> Handle([FromServices] IMediator mediator, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return await mediator.Send(new Get2faKeyQuery(userId));
    }
}
