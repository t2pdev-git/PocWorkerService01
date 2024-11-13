namespace PocWeather.Domain.Weather;

public class WeatherForecast
{
    public string Location { get; init; } = default!;
    public DateTime Date { get; init; }
    public int TemperatureC { get; init; }
    public string Summary { get; init; } = default!;
}