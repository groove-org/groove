using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class Login : IEndpoint
{
    public record LoginRequest(string Email, string Password, bool RememberMe);
    public record LoginResponse(string? Token, bool RequiresTwoFactor);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/login", Handle)
        .WithSummary("Logs in a user and returns a JWT token")
        .WithTags("Identity");

    public static async Task<LoginResponse> Handle([FromServices] IMediator mediator, LoginRequest request)
    {
        var response = await mediator.Send(new LoginCommand(request.Email, request.Password, request.RememberMe));
        return new LoginResponse(response.Token, response.RequiresTwoFactor);
    }
}
