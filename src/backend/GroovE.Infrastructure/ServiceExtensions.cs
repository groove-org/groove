using Ardalis.GuardClauses;
using GroovE.Application;
using GroovE.Application.Data;
using GroovE.Infrastructure.Configuration;
using GroovE.Infrastructure.Data;
using GroovE.Infrastructure.Identity;
using GroovE.Infrastructure.Mailing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroovE.Infrastructure;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddConfiguration<MailingConfiguration>();

        builder.Services.AddSingleton<IEmailSender<User>, InternalMailSenderAdapter>();

        builder.Services.AddSingleton(MailServiceFactory.Create);
        builder.Services.AddSingleton<LoggerMailService>();

        AddDatabase(builder);
    }

    private static void AddDatabase(IHostApplicationBuilder builder)
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
