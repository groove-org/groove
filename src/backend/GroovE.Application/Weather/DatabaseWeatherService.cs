using GroovE.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace GroovE.Application.Weather;

internal class DatabaseWeatherService(IApplicationDataContext context) : IWeatherService
{
    public async Task<string?> GetWeatherDescriptionAsync(string country, CancellationToken cancellationToken)
    {
        var result = await context.WeatherReports.FirstOrDefaultAsync(w => w.Country.ToLower() == country, cancellationToken);
        return result?.Description;
    }
}