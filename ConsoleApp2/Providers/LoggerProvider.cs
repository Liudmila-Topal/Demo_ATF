using ConsoleApp2.Hooks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConsoleApp2.Providers;

public static class LoggerProvider
{
    public static ILogger Logger
    {
        get
        {
            if (GeneralHooks.ServiceProvider == null)
            {
                throw new InvalidOperationException("ServiceProvider is not initialized. Ensure that BeforeFeature has been called.");
            }

            return GeneralHooks.ServiceProvider.GetRequiredService<ILogger>();
        }
    }
}