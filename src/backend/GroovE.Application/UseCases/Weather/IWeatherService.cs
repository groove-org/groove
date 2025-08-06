namespace GroovE.Application.UseCases.Weather;

public interface IWeatherService
{
    public Task<string?> GetWeatherDescriptionAsync(string country, CancellationToken cancellationToken);
}
