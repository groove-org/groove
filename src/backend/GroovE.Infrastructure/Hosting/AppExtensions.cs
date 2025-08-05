using GroovE.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;

namespace GroovE.Infrastructure.Hosting;

public static class AppExtensions
{
    public static Task UseInfrastructure(this WebApplication app) => app.InitialiseDatabaseAsync();
}
