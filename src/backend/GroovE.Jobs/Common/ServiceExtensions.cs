using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GroovE.Jobs.Common;

public static class ServiceExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        var jobTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IJob).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
            .ToList();

        foreach (var jobType in jobTypes)
        {
            services.AddTransient(jobType);
        }

        return services;
    }
}
