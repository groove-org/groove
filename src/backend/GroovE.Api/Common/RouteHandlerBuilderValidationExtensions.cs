using GroovE.Api.Common.Filters;

namespace GroovE.Api.Common;

public static class RouteHandlerBuilderValidationExtensions
{
    /// <summary>
    /// Adds a request validation filter to the route handler.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="builder"></param>
    /// <returns>A <see cref="RouteHandlerBuilder"/> that can be used to futher customize the endpoint.</returns>
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder) => builder
            .AddEndpointFilter<RequestValidationFilter<TRequest>>()
            .ProducesValidationProblem();
}
