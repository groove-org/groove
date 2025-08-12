using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class ResendConfirmationEmailEndpoint : IEndpoint
{
    public record ResendConfirmationEmailRequest(string Email);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/resend-confirmation-email", Handle)
        .WithSummary("Resends the confirmation email")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, [FromBody] ResendConfirmationEmailRequest request)
    {
        await mediator.Send(new ResendConfirmationEmailCommand(request.Email));
    }
}
