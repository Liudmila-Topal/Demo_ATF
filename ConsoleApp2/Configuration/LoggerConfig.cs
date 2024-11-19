using Serilog;

namespace ConsoleApp2.Configuration
{

    public class LoggerConfig
    {
        private static readonly string LogsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFilePath = Path.Combine(LogsDirectory, "logs.txt");
        
        public static void InitializeLogger()
        {
            EnsureLogDirectoryExists();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Уровень логирования
                .WriteTo.Console()
                .WriteTo.File(LogFilePath, rollingInterval: RollingInterval.Day) // Ежедневное логирование
                .CreateLogger();

            Log.Information("Logger initialized successfully.");
        }

        private static void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(LogsDirectory))
            {
                Directory.CreateDirectory(LogsDirectory);
                Console.WriteLine($"Logs directory created: {LogsDirectory}");
            }
            else
            {
                Console.WriteLine($"Logs directory already exists: {LogsDirectory}");
            }
        }
        
        
        // public static void InitializeLogger()
        // {
        //     Log.Logger = new LoggerConfiguration()
        //         .MinimumLevel.Debug() // Уровень логирования
        //         .WriteTo.Console()    // Логирование в консоль
        //         // .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Логирование в файл
        //         .CreateLogger();
        // }
    }
}