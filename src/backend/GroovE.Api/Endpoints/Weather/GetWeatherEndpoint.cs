using FluentValidation;
using GroovE.Api.Common;
using GroovE.Application.Weather;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GroovE.Api.Endpoints.Weather;

public class GetWeatherEndpoint : IEndpoint
{
    public record Request(string Country);
    public record Response(string Description);

    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator() => RuleFor(c => c.Country).NotEmpty();
    }

    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("/", Handle)
        .WithRequestValidation<Request>()
        .WithSummary("Returns the weather");

    public static async Task<Results<Ok<Response>, NotFound>> Handle([AsParameters] Request request, IWeatherService service, CancellationToken cancellationToken)
    {
        var result = await service.GetWeatherDescriptionAsync(request.Country, cancellationToken);
        if (result is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(new Response(result));
    }
}