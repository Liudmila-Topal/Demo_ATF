using ConsoleApp2.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Serilog;
using TechTalk.SpecFlow;

namespace ConsoleApp2.Hooks;

[Binding]
public class GeneralHooks
{
    public static IServiceProvider? ServiceProvider;
    
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        Console.WriteLine("BeforeFeature hook executed.");
        // Проверяем, что ServiceProvider ещё не был настроен
        if (ServiceProvider == null)
        {
            var services = new ServiceCollection();

            // Настройка DI
            services.ConfigureServices(featureContext);

            // Построение провайдера DI
            ServiceProvider = services.BuildServiceProvider();
        }
    }

    [AfterFeature]
    public static void AfterFeature()
    {
        Log.CloseAndFlush(); // Завершение работы Serilog
    }
    
    [AfterTestRun]
    public static void GenerateLivingDocReport()
    {
        try
        {
            var configManager = new ReportConfig();
            var outputFolder = configManager.GetReportOutputFolder();
            var reportGenerator = new ReportGenerator(outputFolder);

            reportGenerator.GenerateLivingDocReport();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating LivingDoc report: {ex.Message}");
        }
    }
}