using PocWeather.Application.Weather;
using PocWeather.Infrastructure.Weather;
using PocWeather.WorkerService01;

// :DLO:0 Create Windows Service using BackgroundService https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<WeatherRetriever>();
builder.Services.AddTransient<IWeatherService, AccuweatherService>();

ConfigureLocationOptions(builder);
ConfigureRetrievalIntervalOptions(builder);

var host = builder.Build();
host.Run();

public partial class Program
{
    private static void ConfigureLocationOptions(HostApplicationBuilder builder)
    {
        const string key = "LocationOptions";
        var locationsSection = 
            builder.Configuration.GetSection(key)
            ?? throw new InvalidOperationException(
                $"Config value(s) for '{key}' is missing");
        builder.Services.Configure<LocationOptions>(locationsSection);
    }    
    
    private static void ConfigureRetrievalIntervalOptions(HostApplicationBuilder builder)
    {
        const string key = "RetrievalIntervalOptions";
        var locationsSection = 
            builder.Configuration.GetSection(key)
            ?? throw new InvalidOperationException(
                $"Config value(s) for '{key}' is missing");
        builder.Services.Configure<RetrievalIntervalOptions>(locationsSection);
    }

    
    
}