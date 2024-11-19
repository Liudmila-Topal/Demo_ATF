using Serilog;

namespace ConsoleApp2.Configuration
{

    public class LoggerConfig
    {
        public static void InitializeLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Уровень логирования
                .WriteTo.Console()    // Логирование в консоль
                // .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Логирование в файл
                .CreateLogger();
        }
    }
}