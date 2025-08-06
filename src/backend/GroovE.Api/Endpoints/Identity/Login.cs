using GroovE.Api.Common;
using GroovE.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class Login : IEndpoint
{
    public record CustomLoginRequest(string Email, string Password, bool RememberMe);
    public record CustomLoginResponse(string Token);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/auth/login", Handle)
        .WithSummary("Logs in a user and returns a JWT token")
        .WithTags("Auth");

    public static async Task<CustomLoginResponse> Handle([FromServices] IMediator mediator, CustomLoginRequest request)
    {
        var response = await mediator.Send(new LoginRequest(request.Email, request.Password, request.RememberMe));
        return new CustomLoginResponse(response.Token);
    }
}
