using FluentValidation;
using Serilog;

namespace GroovE.Api;

public static class ServiceExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddSerilog();
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        AddSwagger(services);
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
            options.InferSecuritySchemes();
        });
    }
}
