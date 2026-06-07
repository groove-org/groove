using System.Reflection;

namespace GroovE.Api.Common;

public static class RouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpointTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);

        foreach (var endpointType in endpointTypes)
        {
            var mapMethod = endpointType.GetMethod("Map", BindingFlags.Public | BindingFlags.Static);
            var returnedBuilder = mapMethod?.Invoke(null, [builder]);
            var endpointName = endpointType.Name.Replace("Endpoint", "");

            if (returnedBuilder is not RouteHandlerBuilder endpointBuilder)
                continue;

            endpointBuilder.WithName(endpointName);
        }
    }
}
