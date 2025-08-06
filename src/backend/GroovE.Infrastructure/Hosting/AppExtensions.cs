using GroovE.Infrastructure.Data;
using Microsoft.Extensions.Hosting;

namespace GroovE.Infrastructure.Hosting;

public static class AppExtensions
{
    public static Task UseInfrastructure(this IHost app) => app.InitializeDatabaseAsync();
}
