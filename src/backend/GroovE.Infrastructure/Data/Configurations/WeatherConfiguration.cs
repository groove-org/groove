using GroovE.Domain.Models.Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroovE.Infrastructure.Data.Configurations;

public class WeatherConfiguration : IEntityTypeConfiguration<WeatherReport>
{
    public void Configure(EntityTypeBuilder<WeatherReport> builder) => builder
        .Property(w => w.Description)
        .IsRequired();
}
