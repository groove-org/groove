using GroovE.Api;
using GroovE.Application;
using GroovE.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddPresentation();

builder.Services.AddSerilog();

builder.Logging.AddFilter("Microsoft.AspNetCore.Authentication", LogLevel.Debug);


var app = builder.Build();

await app.UseInfrastructure();
app.UsePresentation();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunAsync();