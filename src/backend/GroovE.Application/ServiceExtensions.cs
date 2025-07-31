using GroovE.Application.Weather;
using Microsoft.Extensions.DependencyInjection;

namespace GroovE.Application;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services) => services.AddTransient<IWeatherService, DatabaseWeatherService>();
}
