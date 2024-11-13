using PocWeather.Application.Weather;
using PocWeather.Domain.Weather;

namespace PocWeather.Infrastructure.Weather;

public class AccuweatherService : IWeatherService
{
    public async Task<WeatherForecast> GetWeatherForecast(string location)
    {
        // This simulates a call to an external service possibly using HttpClient
        await Task.Delay(1000);

        // Simulate a random temperature
        var rng = new Random();
        var temperatureC = rng.Next(-20, 50);

        return new WeatherForecast
        {
            Location = location,
            Date = DateTime.Now,
            TemperatureC = temperatureC,
            Summary = Summaries[rng.Next(Summaries.Length)]
        };
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

}