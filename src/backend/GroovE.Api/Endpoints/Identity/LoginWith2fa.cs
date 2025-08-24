using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class LoginWith2fa : IEndpoint
{
    public record LoginWith2faRequest(string Email, string TwoFactorCode);
    public record LoginWith2faResponse(string Token);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/login/2fa", Handle)
        .WithSummary("Logs in a user with a 2FA code")
        .WithTags("Identity");

    public static async Task<LoginWith2faResponse> Handle([FromServices] IMediator mediator, [FromBody] LoginWith2faRequest request)
    {
        var response = await mediator.Send(new LoginWith2faCommand(request.Email, request.TwoFactorCode));
        return new LoginWith2faResponse(response.Token);
    }
}
