// CADCleanser
// "Streamline your CAD projects with CADCleanser. Detect and resolve issues in SOLIDWORKS files to ensure seamless collaboration and efficient workflows."

using CADCleanser;
using CADCleanser.Services;
using CADCleanser.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

string baseDirectory = @"K:\Operations\Modular\Special Builds\Belts\";

// If necessary, use UNC path instead of the mapped drive
// string baseDirectory = @"\\ServerName\ShareName\Operations\Modular\Special Builds\Belts\Habasit Link AutoCad Drawing Templates for Modular Belts";

// Validate the provided directory path
if (!Directory.Exists(baseDirectory))
{
    Console.WriteLine($"The directory '{baseDirectory}' does not exist.");
    return;
}

// Set up dependency injection
var serviceProvider = new ServiceCollection()
    .AddSingleton<ILoggingService, LoggingService>()
    .AddSingleton<ISolidWorksService, SolidWorksService>()
    .AddSingleton<IDwgProcessingService, DwgProcessingService>()
    .AddSingleton<IFileProcessor, FileProcessor>()
    .AddSingleton<DirectoryProcessor>()
    .AddSingleton<ReportGenerator>()
    .BuildServiceProvider();

var logger = serviceProvider.GetService<ILogger<Program>>();
Console.WriteLine("Application started.");

//// Process the directory
//var directoryProcessor = serviceProvider.GetService<DirectoryProcessor>();
//directoryProcessor.ProcessDirectory(directoryPath);

//// Generate the HTML report on the desktop
//var reportGenerator = serviceProvider.GetService<ReportGenerator>();
//string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//string reportPath = Path.Combine(desktopPath, "CADCleanser_Report.html");
//reportGenerator.GenerateHtmlReport(reportPath);

var solidWorksService = serviceProvider.GetRequiredService<ISolidWorksService>();
var dwgProcessingService = serviceProvider.GetRequiredService<IDwgProcessingService>();
var loggingService = serviceProvider.GetRequiredService<ILoggingService>();

try
{
    string jsonFilePath = "C:\\localrepos\\DrawingNumberGenerator\\Client\\wwwroot\\Data\\output.json"; // Replace with actual path
    var files = JsonParser.ParseJson(jsonFilePath);

    loggingService.LogInfo($"Total files to process: {files.Count}");

    foreach (var file in files)
    {
        try
        {
            loggingService.LogInfo($"Processing file: {file.Filename}");

            // Construct the full file path
            string filePath = System.IO.Path.Combine(baseDirectory, file.RelativePath);

            // Extract blocks from DWG file
            var blocks = dwgProcessingService.ExtractBlocks(filePath);
            loggingService.LogInfo($"Found {blocks.Count} blocks in {file.Filename}");

            foreach (var blockName in blocks)
            {
                try
                {
                    // Save each block to SolidWorks Design Library
                    solidWorksService.SaveBlockToLibrary(blockName, "Plastic Modular");
                    loggingService.LogInfo($"Successfully saved block '{blockName}' to Design Library.");
                }
                catch (Exception ex)
                {
                    loggingService.LogError($"Failed to save block '{blockName}' from file '{file.Filename}'.", ex);
                }
            }
        }
        catch (Exception ex)
        {
            loggingService.LogError($"Failed to process file '{file.Filename}'.", ex);
        }
        finally
        {
            solidWorksService.CloseFile();
        }
    }
}
catch (Exception ex)
{
    loggingService.LogError("An unexpected error occurred.", ex);
}
finally
{
    loggingService.FinalizeLog();
}

//Console.WriteLine($"Report generated at: {reportPath}");

Console.WriteLine("Application ended.");