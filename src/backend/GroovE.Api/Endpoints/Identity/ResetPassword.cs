using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class ResetPassword : IEndpoint
{
    public record ResetPasswordRequest(string Email, string Token, string NewPassword);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/reset-password", Handle)
        .WithSummary("Resets the user's password")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, [FromBody] ResetPasswordRequest request)
    {
        await mediator.Send(new ResetPasswordCommand(request.Email, request.Token, request.NewPassword));
    }
}
