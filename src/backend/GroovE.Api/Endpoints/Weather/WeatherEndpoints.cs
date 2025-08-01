using GroovE.Api.Common;

namespace GroovE.Api.Endpoints.Weather;

public static class WeatherEndpoints
{
    internal static void MapWeatherEndpoints(this IEndpointRouteBuilder app) => app
        .MapGroup("/weather")
            .MapEndpoint<GetWeatherEndpoint>();
}
