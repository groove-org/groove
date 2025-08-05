using Ardalis.GuardClauses;
using GroovE.Application.Data;
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
        builder.Services.Configure<MailingConfiguration>(builder.Configuration.GetSection(nameof(MailingConfiguration)));
        builder.Services.AddSingleton<IEmailSender<User>, InternalMailSenderAdapter>();
        builder.Services.AddSingleton(MailServiceFactory.Create);
        builder.Services.AddSingleton<LoggerMailService>();

        var section = builder.Configuration.GetSection(nameof(JwtConfiguration));
        builder.Services.Configure<JwtConfiguration>(section);
        builder.Services.AddScoped<Application.UseCases.Auth.IAuthenticationService, Identity.AuthenticationService>();

        builder.Services.AddIdentityApiEndpoints<User>();

        var jwtSettings = section.Get<JwtConfiguration>() ?? new();
        builder.Services.AddGroovEAuthentication(jwtSettings);
        builder.Services.AddAuthorizationBuilder()
            .AddGroovEPolicies();

        builder.AddDatabase();
    }

    private static AuthenticationBuilder AddGroovEAuthentication(this IServiceCollection services, JwtConfiguration jwtSettings)
        => services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

    private static void AddGroovEPolicies(this AuthorizationBuilder builder) => builder
        .AddPolicy(Policies.AdminOnly, policy =>
        {
            policy.RequireRole(Roles.Admin);
        })
        .SetDefaultPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build());

    private static void AddDatabase(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");
        Guard.Against.Null(connectionString, message: "Connection string 'Sqlite' not found.");

        builder.Services.AddDbContext<IApplicationDataContext, DatabaseContext>((options) =>
        {
            options.UseSqlite(connectionString);
        });

        builder.Services.AddScoped<DatabaseContextInitializer>();

        builder.Services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>();
    }
}
