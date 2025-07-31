using GroovE.Api.Common;

namespace GroovE.Api.Endpoints.Weather;

public static class WeatherEndpoints
{
    internal static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/weather")
            .WithTags("Weather");

        endpoints.MapPublicGroup()
            .MapEndpoint<GetWeatherEndpoint>();
    }
}
