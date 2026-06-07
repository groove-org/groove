using GroovE.Api.Common;
using GroovE.Application.Identity;
using GroovE.Application.UseCases.Weather;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace GroovE.Api.Endpoints.Weather;

public class GetWeatherEndpoint : IEndpoint
{
    public record Request(string Country);
    public record Response(string Description);

    public static RouteHandlerBuilder Map(IEndpointRouteBuilder app) => app
        .MapGet("/", Handle)
        .WithSummary("Returns the weather")
        .RequireAuthorization(Policies.AdminOnly);

    public static async Task<Results<Ok<Response>, NotFound>> Handle([AsParameters] Request request, ClaimsPrincipal claimsPrincipal, IWeatherService service, CancellationToken cancellationToken)
    {
        var result = await service.GetWeatherDescriptionAsync(request.Country, cancellationToken);
        if (result is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(new Response(result));
    }
}