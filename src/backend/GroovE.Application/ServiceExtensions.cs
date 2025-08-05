using GroovE.Application.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroovE.Application;

public static class ServiceExtensions
{
    public static void AddApplication(this IHostApplicationBuilder builder) => builder.Services.AddTransient<IWeatherService, DatabaseWeatherService>();
}
