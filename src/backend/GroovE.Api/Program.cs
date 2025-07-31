using GroovE.Api;
using GroovE.Application;
using GroovE.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddPresentation();
builder.Services.AddApplication();
builder.AddInfrastructure();

builder.Services.AddSerilog();

var app = builder.Build();

app.UsePresentation();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunAsync();

// TODO:
// All filters
// Authentication / Authorization
// Login and User endpoints