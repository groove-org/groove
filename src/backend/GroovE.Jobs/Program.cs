using GroovE.Application.Hosting;
using GroovE.Infrastructure.Hosting;
using GroovE.Jobs.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

var jobTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(IJob).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
    .ToList();

if (args.Length == 0 || args[0] is "-h" or "--help")
{
    Console.WriteLine("Available jobs:");
    foreach (var jobType in jobTypes)
    {
        Console.WriteLine($"  {jobType.Name.ToLowerInvariant()} - Run the {jobType.Name} job");
    }

    Console.WriteLine("\nUsage: GroovE.Jobs.exe <job>");
    return;
}

var jobName = args[0].ToLowerInvariant();
var selectedJobType = jobTypes.FirstOrDefault(j => j.Name.ToLowerInvariant() == jobName);
if (selectedJobType == null)
{
    Console.WriteLine($"Unknown job: {jobName}");
    Console.WriteLine("Use -h or --help to list available jobs.");
    return;
}

var builder = Host.CreateApplicationBuilder();

builder.AddInfrastructure();
builder.AddApplication();

builder.Services.AddSerilog(options => options.WriteTo.Console());
builder.Services.AddJobs();

var app = builder.Build();

await app.UseInfrastructure();

var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopeFactory!.CreateScope();
var job = (IJob)scope.ServiceProvider.GetRequiredService(selectedJobType);
await job.RunAsync();