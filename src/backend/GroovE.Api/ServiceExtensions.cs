using FluentValidation;
using GroovE.Api.Common;
using GroovE.Api.Endpoints.Identity;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

namespace GroovE.Api;

public static class ServiceExtensions
{
    public static void AddPresentation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog();
        builder.Services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
        builder.Services.AddIdentityApiEndpoints<User>();

        var section = builder.Configuration.GetSection(nameof(JwtSettings));
        builder.Services.Configure<JwtSettings>(section);

        var jwtSettings = section.Get<JwtSettings>() ?? new();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicies();

        AddSwagger(builder);

        builder.Services.AddEndpointsFromAssembly(typeof(ServiceExtensions).Assembly);
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        //builder.Services.AddControllers();
    }

    private static void AddPolicies(this AuthorizationBuilder builder) => builder
        .AddPolicy(Policies.AdminOnly, policy =>
        {
            policy.RequireRole(Roles.Admin);
        })
        .SetDefaultPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());

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

    public static IServiceCollection AddEndpointsFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var endpointTypes = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var type in endpointTypes)
        {
            services.AddScoped(typeof(IEndpoint), type);
        }

        return services;
    }
}
