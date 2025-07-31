using GroovE.Api.Endpoints.Weather;

namespace GroovE.Api;

public static class EndpointConfigurations
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("")
            .WithOpenApi();

        endpoints.MapWeatherEndpoints();
    }
}
