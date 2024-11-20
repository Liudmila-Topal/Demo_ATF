using System.Diagnostics;

namespace ConsoleApp2.Configuration;

public class ReportGenerator
{
    private readonly string _outputFolder;

    public ReportGenerator(string outputFolder)
    {
        _outputFolder = outputFolder ?? throw new ArgumentNullException(nameof(outputFolder));
    }

    public void GenerateLivingDocReport()
    {
        try
        {
            string assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net8.0", "ConsoleApp2.dll");
            // string assemblyPath = @"\C:\\Users\\Liudmila.Prepelita\\RiderProjects\\ConsoleApp2\\ConsoleApp2\\bin\\Debug\\net8.0\\ConsoleApp2.dll\";
            // string assemblyPath = "ConsoleApp2/bin/Debug/net8.0/ConsoleApp2.dll";

            // Проверяем существование сборки
            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException("Assembly file not found.", assemblyPath);
            }

            // Создаем папку, если её нет
            if (!Directory.Exists(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }

            string command = $"dotnet livingdoc test-assembly \"{assemblyPath}\" -o \"{_outputFolder}\"";

            Console.WriteLine($"Generating LivingDoc report at: {_outputFolder}");
            Console.WriteLine($"Executing command: {command}");

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"livingdoc test-assembly \"{assemblyPath}\" -o \"{_outputFolder}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                process.Start();

                // Логируем стандартный вывод и ошибки
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Report generation failed with exit code {process.ExitCode}. Error: {error}");
                }

                Console.WriteLine(output);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while generating the LivingDoc report: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }
}