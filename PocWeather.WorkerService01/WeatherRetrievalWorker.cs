using Microsoft.Extensions.Options;
using PocWeather.Application.Weather;

namespace PocWeather.WorkerService01
{
    public class WeatherRetrievalWorker : BackgroundService
    {
        private readonly WeatherRetriever _weatherRetriever;
        private readonly RetrievalIntervalOptions _retrievalIntervalOptions;
        private readonly ILogger<WeatherRetrievalWorker> _logger;

        public WeatherRetrievalWorker(
            WeatherRetriever weatherRetriever, 
            IOptions<RetrievalIntervalOptions> retrievalIntervalOptions,
            ILogger<WeatherRetrievalWorker> logger)
        {
            _weatherRetriever = weatherRetriever;
            _retrievalIntervalOptions = retrievalIntervalOptions.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    IEnumerable<WeatherForecastDto> weatherForecasts = 
                        await _weatherRetriever.GetWeatherForecast(); 
                
                    _logger.LogInformation(
                        "Retrieved succeeded: {ForecastCount} forecasts returned", weatherForecasts.Count());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occured: {ExceptionMessage}", ex.Message);
                }
                
                await Task.Delay(
                    TimeSpan.FromSeconds(_retrievalIntervalOptions.RetrieveIntervalSeconds), 
                    stoppingToken);
            }
        }
    }
}
