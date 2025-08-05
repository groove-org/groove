using GroovE.Api.Hosting;
using GroovE.Application.Hosting;
using GroovE.Infrastructure.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddInfrastructure();
builder.AddApplication();
builder.AddPresentation();

builder.Services.AddSerilog(options => options.WriteTo.Console());

var app = builder.Build();

await app.UseInfrastructure();
app.UsePresentation();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunAsync();