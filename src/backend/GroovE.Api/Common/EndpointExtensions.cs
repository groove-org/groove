namespace GroovE.Api.Common;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var svc = scope.ServiceProvider;
        var endpoints = svc.GetServices<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.Map(app);
        }

        return app;
    }
}
