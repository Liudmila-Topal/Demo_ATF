using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;

namespace ConsoleApp2.Configuration;

public class ReportConfig
{
    public IConfiguration Configuration { get; }

    public ReportConfig()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Корневой каталог проекта
            .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
            .Build();
    }
    
    public string GetReportOutputFolder() => Configuration["SpecFlow:Report:OutputFolder"] ?? "Reports";
}