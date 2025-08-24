using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class ConfirmEmail : IEndpoint
{
    public record ConfirmEmailRequest([FromQuery] string UserId, [FromQuery] string Code);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("/identity/confirmEmail", Handle)
        .WithSummary("Confirms a user's email address")
        .WithTags("Identity");

    public static async Task<IResult> Handle([FromServices] IMediator mediator, [AsParameters] ConfirmEmailRequest request)
    {
        await mediator.Send(new ConfirmEmailCommand(request.UserId, request.Code));
        return Results.Redirect("/");
    }
}
