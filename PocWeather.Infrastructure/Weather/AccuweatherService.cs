using PocWeather.Application.Weather;
using PocWeather.Domain.Weather;

namespace PocWeather.Infrastructure.Weather;

public class AccuweatherService : IWeatherService
{
    public async Task<WeatherForecast> GetWeatherForecast(string location)
    {
        // This simulates a call to an external service possibly using HttpClient
        await Task.Delay(1000);
        
        // Simulate a random API call failure
        Random randomFailure = new ();
        var randomFailureValue = randomFailure.Next(1, 10);
        if (randomFailureValue <= 3)
        {
            throw new SimulatedApiFailureException("Simulating an API failure");
        }

        // Simulate a random temperature
        Random randomTemperature = new ();
        var temperatureC = randomTemperature.Next(-20, 50);

        return new WeatherForecast
        {
            Location = location,
            Date = DateTime.Now,
            TemperatureC = temperatureC,
            Summary = Summaries[randomTemperature.Next(Summaries.Length)]
        };
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
}

public class SimulatedApiFailureException(string simulatingAnApiFailure) : Exception(simulatingAnApiFailure);