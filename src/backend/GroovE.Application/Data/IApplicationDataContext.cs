using GroovE.Domain.Models.Weather;
using Microsoft.EntityFrameworkCore;

namespace GroovE.Application.Data;

public interface IApplicationDataContext
{
    public DbSet<WeatherReport> WeatherReports { get; set; }
}
