using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DrawingFinder
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information))
                .AddSingleton<ICADLibraryService, CADLibraryService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var reportService = serviceProvider.GetRequiredService<ICADLibraryService>();

            // Define target directory and output paths
            string targetDirectory = @"K:\Operations\Modular\Special Builds\Belts";
            string jsonReportPath = "pdf_dwg_drawings.json";
            string htmlReportPath = "report.html";
            int categoryDepth = 2; // Example depth level for categorization

            try
            {
                // Process the directory to get file statistics
                logger.LogInformation($"Starting to process the directory: {targetDirectory}");
                var header = await reportService.GenerateLibraryReportAsync(targetDirectory, categoryDepth);

                if (header == null)
                {
                    logger.LogError("Directory processing failed. No data to report.");
                    return;
                }

                // Generate JSON and HTML reports
                await reportService.SaveLibraryJsonReportAsync(jsonReportPath, header, header);
                logger.LogInformation($"JSON report generated at: {jsonReportPath}");

                await reportService.SaveLibraryHtmlReportAsync(htmlReportPath, header, header);
                logger.LogInformation($"HTML report generated at: {htmlReportPath}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during the report generation process.");
            }
        }
    }
}