using FluentValidation;
using GroovE.Application.Common.Behaviors;
using GroovE.Application.Data;
using GroovE.Application.UseCases.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GroovE.Application.Hosting;

public static class ServiceExtensions
{
    public static void AddApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<IApplicationDataContext>()
            .AddOpenBehavior(typeof(ValidationBehaviour<,>)));

        builder.Services.AddTransient<IWeatherService, DatabaseWeatherService>();
        builder.Services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
    }
}
