using Microsoft.Extensions.Options;
using PocWeather.Application.Weather;

namespace PocWeather.WorkerService01
{
    public class Worker : BackgroundService
    {
        private readonly WeatherRetriever _weatherRetriever;
        private readonly RetrievalIntervalOptions _retrievalIntervalOptions;
        private readonly ILogger<Worker> _logger;

        public Worker(
            WeatherRetriever weatherRetriever, 
            IOptions<RetrievalIntervalOptions> retrievalIntervalOptions,
            ILogger<Worker> logger)
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
                
                IEnumerable<WeatherForecastDto> weatherForecasts = 
                    await _weatherRetriever.GetWeatherForecast(); 
                
                _logger.LogInformation("Retrieved {ForecastCount} forecasts.", weatherForecasts.Count());
                
                await Task.Delay(
                    TimeSpan.FromSeconds(_retrievalIntervalOptions.RetrieveIntervalSeconds), 
                    stoppingToken);
            }
        }
    }
}
