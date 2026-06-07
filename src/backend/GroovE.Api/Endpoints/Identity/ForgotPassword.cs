using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class ForgotPassword : IEndpoint
{
    public record ForgotPasswordRequest(string Email);

    public static RouteHandlerBuilder Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/forgot-password", Handle)
        .WithSummary("Sends a password reset link to the user's email")
        .WithTags("Identity");

    public static async Task Handle([FromServices] IMediator mediator, [FromBody] ForgotPasswordRequest request) => await mediator.Send(new ForgotPasswordCommand(request.Email));
}
