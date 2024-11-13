using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PocWeather.Application.Weather;

public class WeatherRetriever
{
    private readonly IWeatherService _weatherService;
    private readonly LocationOptions _locationOptions;
    private readonly ILogger<WeatherRetriever> _logger;

    public WeatherRetriever(
        IWeatherService weatherService, 
        IOptions<LocationOptions> locationOptions,
        ILogger<WeatherRetriever> logger)
    {
        _weatherService = weatherService;
        _locationOptions = locationOptions.Value;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecast()
    {
        List<WeatherForecastDto> results = [];
        foreach (var location in _locationOptions.Locations)
        {
            _logger.LogInformation("Start: Getting weather forecast for {Location}", location);

            try
            {
                var x = await _weatherService.GetWeatherForecast(location);
                results.Add(new WeatherForecastDto(x.Location, x.Date, x.TemperatureC, x.Summary));
            
                _logger.LogInformation("Completed: Getting weather forecast for {Location}", location);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Failed getting weather forecast for {Location}. Exception Message: {ExceptionMessage}", 
                    location, ex.Message);
            }
        }
        
        return results;
    }
}