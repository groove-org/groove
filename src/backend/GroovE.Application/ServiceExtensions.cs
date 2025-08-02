using GroovE.Application.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroovE.Application;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services) => services.AddTransient<IWeatherService, DatabaseWeatherService>();

    public static void AddConfiguration<TConfiguration>(this IServiceCollection services) where TConfiguration : class, new()
    {
        var configuration = new TConfiguration();
        var configurationSectionName = typeof(TConfiguration).Name.Replace("Configuration", string.Empty);

        services.AddSingleton(sp =>
        {
            var configurationSection = sp.GetRequiredService<IConfiguration>().GetSection(configurationSectionName);
            configurationSection.Bind(configuration);
            return configuration;
        });
    }
}
