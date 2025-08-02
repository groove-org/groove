using FluentValidation;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace GroovE.Api;

public static class ServiceExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddSerilog();
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
        services.AddIdentityApiEndpoints<User>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        });


        services.AddAuthorizationBuilder()
            .AddPolicies();

        AddSwagger(services);
    }

    private static void AddPolicies(this AuthorizationBuilder builder) => builder
        .AddPolicy(Policies.AdminOnly, policy =>
        {
            //policy.RequireAuthenticatedUser();
            policy.RequireRole(Roles.Admin);
        })
        .SetFallbackPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());

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
