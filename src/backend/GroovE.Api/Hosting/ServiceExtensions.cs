using Microsoft.OpenApi.Models;
using Serilog;

namespace GroovE.Api.Hosting;

public static class ServiceExtensions
{
    public static void AddPresentation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog();
        AddSwagger(builder);

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }

    private static void AddSwagger(IHostApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
