namespace PocWeather.Application.Weather;

public record WeatherForecastDto(string Location, DateTime Date, int TemperatureC, string Summary);
