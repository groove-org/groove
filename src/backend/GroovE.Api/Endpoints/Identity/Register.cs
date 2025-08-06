using GroovE.Api.Common;
using GroovE.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class Register : IEndpoint
{
    public record CustomRegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record CustomRegisterResponse(string Token);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/auth/register", Handle)
        .WithSummary("Registers a new user and returns a JWT token")
        .WithTags("Auth");

    public static async Task<CustomRegisterResponse> Handle([FromServices] IMediator mediator, CustomRegisterRequest request)
    {
        var response = await mediator.Send(new RegisterRequest(request.Email, request.Password, request.FirstName, request.LastName));
        return new CustomRegisterResponse(response.Token);
    }
}
