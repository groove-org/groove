using GroovE.Application.Common;
using GroovE.Application.Data;
using GroovE.Application.Identity;
using GroovE.Infrastructure.Content;
using GroovE.Infrastructure.Data;
using GroovE.Infrastructure.Identity;
using GroovE.Infrastructure.Mailing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GroovE.Infrastructure.Hosting;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        AddMailing(builder);
        AddContentService(builder);

        builder.Services.AddHttpContextAccessor();

        var section = builder.Configuration.GetSectionWithoutSuffix<JwtConfiguration>();
        builder.Services.Configure<JwtConfiguration>(section);
        builder.Services.AddScoped<Application.UseCases.Identity.IIdentityService, Identity.IdentityService>();

        builder.Services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = section.Get<JwtConfiguration>() ?? new();
        builder.Services.AddGroovEAuthentication(jwtSettings);
        builder.Services.AddAuthorizationBuilder()
            .AddPolicies();

        AddDatabase(builder);
    }

    private static void AddMailing(IHostApplicationBuilder builder)
    {
        builder.Services.Configure<MailingConfiguration>(builder.Configuration.GetSectionWithoutSuffix<MailingConfiguration>());
        builder.Services.AddSingleton(MailServiceFactory.Create);
        builder.Services.AddSingleton<LoggerMailService>();
    }

    private static void AddContentService(IHostApplicationBuilder builder)
    {
        builder.Services.Configure<ContentConfiguration>(builder.Configuration.GetSectionWithoutSuffix<ContentConfiguration>());
        builder.Services.AddSingleton(ContentServiceFactory.Create);
    }

    private static AuthenticationBuilder AddGroovEAuthentication(this IServiceCollection services, JwtConfiguration jwtSettings)
        => services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
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

    private static void AddPolicies(this AuthorizationBuilder builder) => builder
        .AddPolicy(Policies.AdminOnly, policy =>
        {
            policy.RequireRole(Roles.Admin);
        })
        .SetDefaultPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());

    private static void AddDatabase(IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration.GetSectionWithoutSuffix<DatabaseConfiguration>().Get<DatabaseConfiguration>() ?? new();

        builder.Services.AddDbContext<IApplicationDataContext, DatabaseContext>((options) =>
        {
            switch (configuration.Provider)
            {
                case DatabaseProvider.SQLite:
                    options.UseSqlite(configuration.ConnectionString);
                    break;
                case DatabaseProvider.PostgreSQL:
                    options.UseNpgsql(configuration.ConnectionString);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported database provider: {configuration.Provider}");
            }
        });

        builder.Services.AddScoped<DatabaseContextInitializer>();
    }
}
