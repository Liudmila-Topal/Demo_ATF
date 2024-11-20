// using ConsoleApp2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Serilog;
using TechTalk.SpecFlow;

namespace ConsoleApp2.Configuration;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, FeatureContext featureContext)
    {
        // Загрузка конфигурации из appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("logConfigurations.json", optional: false, reloadOnChange: true)
            .Build();

        // Получение названия feature файла
        string featureFileName = featureContext.FeatureInfo.Tags.FirstOrDefault() ?? "UnknownFeature";

        // Формирование имени файла
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string sanitizedFeatureName = string.Concat(featureFileName.Split(Path.GetInvalidFileNameChars()));
        string logFileName = $"log-{timestamp}-{sanitizedFeatureName}.txt";

        // Директория для логов
        string logsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        Directory.CreateDirectory(logsDirectory);
        string logFilePath = Path.Combine(logsDirectory, logFileName);

        // Настройка Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.File(
                path: logFilePath,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u4}] {Message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Infinite)
            .CreateLogger();


        // Регистрация Serilog.ILogger
        services.AddSingleton<Serilog.ILogger>(Log.Logger);
        
        services.AddSingleton<ReportConfig>();
        
        // // Регистрация Refit клиента
        // services.AddRefitClient<IContactApi>()
        //     .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
        return services;
    }
}