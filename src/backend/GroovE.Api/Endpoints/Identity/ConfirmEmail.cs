using GroovE.Api.Common;
using GroovE.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class ConfirmEmail : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("/identity/confirmEmail", Handle)
        .WithSummary("Confirms a user's email address")
        .WithTags("Identity");

    public static async Task<IResult> Handle([FromServices] IMediator mediator, [FromQuery] string userId, [FromQuery] string code)
    {
        await mediator.Send(new ConfirmEmailRequest(userId, code));
        return Results.Redirect("/");
    }
}
