using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using GroovE.Application.UseCases.Identity.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Identity;

public class GetProfile : IEndpoint
{
    public static RouteHandlerBuilder Map(IEndpointRouteBuilder app) => app
        .MapGet("/identity/profile", Handle)
        .RequireAuthorization()
        .WithSummary("Gets the user's profile")
        .WithTags("Identity");

    public static async Task<UserProfileDto> Handle([FromServices] IMediator mediator, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return await mediator.Send(new GetProfileQuery(userId));
    }
}
