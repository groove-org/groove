using GroovE.Api.Common;
using GroovE.Application.UseCases.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GroovE.Api.Endpoints.Identity;

public class Register : IEndpoint
{
    public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
    public record RegisterResponse(string Token);

    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("/identity/register", Handle)
        .WithSummary("Registers a new user and returns a JWT token")
        .WithTags("Identity");

    public static async Task<RegisterResponse> Handle([FromServices] IMediator mediator, RegisterRequest request)
    {
        var response = await mediator.Send(new RegisterCommand(request.Email, request.Password, request.FirstName, request.LastName));
        return new RegisterResponse(response.Token);
    }
}
