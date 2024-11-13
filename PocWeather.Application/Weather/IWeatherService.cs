using PocWeather.Domain.Weather;

namespace PocWeather.Application.Weather;

public interface IWeatherService
{
    Task<WeatherForecast> GetWeatherForecast(string location);
}