using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DrawingFinder.Models;

namespace DrawingFinder
{
    internal class Program
    {
        private static ILogger<Program> _logger;
        private static int _categoryDepth = 2; // Configurable depth level for categorization
        private static Dictionary<string, int> pdfCategories = new Dictionary<string, int>();
        private static Dictionary<string, int> dwgCategories = new Dictionary<string, int>();

        private static async Task Main(string[] args)
        {
            // Set up logging
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Information);
            });
            _logger = loggerFactory.CreateLogger<Program>();

            // Initialize data structures
            var pdfData = new DirectoryStructure();
            var dwgData = new DirectoryStructure();
            int pdfDrawings = 0, dwgDrawings = 0;
            long pdfTotalSizeBytes = 0, dwgTotalSizeBytes = 0;

            // Define the directory to search
            string targetDirectory = @"K:\Operations\Modular\Special Builds\Belts";
            _logger.LogInformation($"Starting to crawl the directory: {targetDirectory}");

            if (!Directory.Exists(targetDirectory))
            {
                _logger.LogError($"The directory {targetDirectory} does not exist.");
                return;
            }

            // Start measuring runtime
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                // Enumerate all PDF and DWG files, excluding those containing 'do-not' in the name
                var files = Directory.EnumerateFiles(targetDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(file => file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".dwg", StringComparison.OrdinalIgnoreCase));

                ParallelOptions parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };

                Parallel.ForEach(files, parallelOptions, file =>
                {
                    string fileLower = Path.GetFileName(file).ToLower();
                    if (fileLower.Contains("do-not"))
                        return;

                    try
                    {
                        string relativePath = Path.GetRelativePath(targetDirectory, file);
                        FileInfo fileInfo = new FileInfo(file);
                        double sizeMb = Math.Round((double)fileInfo.Length / (1024 * 1024), 2);
                        DateTime createdDate = fileInfo.CreationTime;

                        // Determine category based on depth
                        var relativeDir = Path.GetDirectoryName(relativePath) ?? string.Empty;
                        bool isPdf = fileLower.EndsWith(".pdf");

                        // Update totals and category counts for each file type
                        if (isPdf)
                        {
                            System.Threading.Interlocked.Increment(ref pdfDrawings);
                            System.Threading.Interlocked.Add(ref pdfTotalSizeBytes, fileInfo.Length);
                            lock (pdfData)
                            {
                                AddToDirectoryStructure(pdfData, relativeDir, new FileInfoModel
                                {
                                    Filename = Path.GetFileName(file),
                                    RelativePath = relativePath.Replace("\\", "/"),
                                    SizeMb = sizeMb,
                                    CreatedDate = createdDate.ToString("o")
                                }, pdfCategories);
                            }
                        }
                        else
                        {
                            System.Threading.Interlocked.Increment(ref dwgDrawings);
                            System.Threading.Interlocked.Add(ref dwgTotalSizeBytes, fileInfo.Length);
                            lock (dwgData)
                            {
                                AddToDirectoryStructure(dwgData, relativeDir, new FileInfoModel
                                {
                                    Filename = Path.GetFileName(file),
                                    RelativePath = relativePath.Replace("\\", "/"),
                                    SizeMb = sizeMb,
                                    CreatedDate = createdDate.ToString("o")
                                }, dwgCategories);
                            }
                        }

                        // Log the processed file
                        _logger.LogInformation($"Processed file: {relativePath} | Size: {sizeMb} MB | Created Date: {createdDate:O}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing file {file}: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error during file enumeration: {ex.Message}");
            }

            // Sort the files alphabetically by filename in each directory
            SortFiles(pdfData);
            SortFiles(dwgData);

            // Stop measuring runtime
            stopwatch.Stop();

            // Prepare headers with aggregate info
            var pdfHeader = new Header
            {
                PdfDrawings = pdfDrawings,
                TotalCategories = pdfCategories.Count,
                TotalSizeMb = Math.Round((double)pdfTotalSizeBytes / (1024 * 1024), 2),
                GeneratedDate = DateTime.Now,
                ElapsedRuntimeSeconds = stopwatch.Elapsed.TotalSeconds,
                CategoryCounts = pdfCategories
            };

            var dwgHeader = new Header
            {
                PdfDrawings = dwgDrawings,
                TotalCategories = dwgCategories.Count,
                TotalSizeMb = Math.Round((double)dwgTotalSizeBytes / (1024 * 1024), 2),
                GeneratedDate = DateTime.Now,
                ElapsedRuntimeSeconds = stopwatch.Elapsed.TotalSeconds,
                CategoryCounts = dwgCategories
            };

            // Prepare final data with headers and directory structures
            var outputData = new
            {
                PDF = new { Header = pdfHeader, Directory_Structure = pdfData },
                DWG = new { Header = dwgHeader, Directory_Structure = dwgData }
            };

            // Write the data to a JSON file with improved readability
            var jsonFilePath = "pdf_dwg_drawings.json";
            try
            {
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(outputData, jsonOptions);
                await File.WriteAllTextAsync(jsonFilePath, jsonString);
                _logger.LogInformation("Aggregate info written to pdf_dwg_drawings.json");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing JSON file: {ex.Message}");
            }

            // Generate the HTML report
            var htmlFilePath = "report.html";
            try
            {
                GenerateHtmlReport(htmlFilePath, outputData);
                _logger.LogInformation("HTML report generated.");
                Process.Start(new ProcessStartInfo { FileName = htmlFilePath, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating HTML file: {ex.Message}");
            }

            _logger.LogInformation($"Found {pdfDrawings} PDF drawings in {pdfCategories.Count} categories.");
            _logger.LogInformation($"Found {dwgDrawings} DWG drawings in {dwgCategories.Count} categories.");
            _logger.LogInformation($"Script runtime: {stopwatch.Elapsed.TotalSeconds} seconds");
        }

        private static void AddToDirectoryStructure(DirectoryStructure currentLevel, string relativeDir, FileInfoModel fileInfo, Dictionary<string, int> categories)
        {
            var dirsInPath = relativeDir.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string topLevelCategory = dirsInPath.Length > 0 ? dirsInPath[0] : "Uncategorized";
            string pitchLevel = dirsInPath.Length > 1 ? dirsInPath[1] : "Uncategorized";
            string categoryKey = $"{topLevelCategory} / {pitchLevel}";

            lock (categories)
            {
                if (categories.ContainsKey(categoryKey))
                    categories[categoryKey]++;
                else
                    categories[categoryKey] = 1;
            }

            if (!currentLevel.ContainsKey(topLevelCategory))
                currentLevel[topLevelCategory] = new DirectoryStructure();
            currentLevel = (DirectoryStructure)currentLevel[topLevelCategory];

            if (!currentLevel.ContainsKey(pitchLevel))
                currentLevel[pitchLevel] = new DirectoryStructure();
            currentLevel = (DirectoryStructure)currentLevel[pitchLevel];

            if (!currentLevel.ContainsKey("files"))
                currentLevel["files"] = new List<FileInfoModel>();
            ((List<FileInfoModel>)currentLevel["files"]).Add(fileInfo);
        }

        private static void SortFiles(DirectoryStructure currentLevel)
        {
            if (currentLevel.ContainsKey("files"))
            {
                var files = (List<FileInfoModel>)currentLevel["files"];
                files.Sort((x, y) => string.Compare(x.Filename, y.Filename, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var key in currentLevel.Keys)
            {
                if (key == "files") continue;
                if (currentLevel[key] is DirectoryStructure subDir)
                {
                    SortFiles(subDir);
                }
            }
        }

        private static void GenerateHtmlReport(string htmlFilePath, dynamic outputData)
        {
            var pdfHeader = outputData.PDF.Header;
            var dwgHeader = outputData.DWG.Header;

            // Generate the summary statement
            string summary = GenerateSummary(pdfHeader, dwgHeader);

            var htmlContent = $@"
    <html>
    <head>
        <title>File Report</title>
        <style>
            body {{ font-family: Arial, sans-serif; margin: 20px; }}
            h1, h2 {{ color: #2E8BC0; }}
            .section {{ margin-top: 20px; }}
            table {{ width: 100%; border-collapse: collapse; }}
            th, td {{ padding: 8px; text-align: left; border-bottom: 1px solid #ddd; }}
            .category {{ color: #555; font-weight: bold; }}
            .summary {{ font-size: 1.1em; color: #333; margin-bottom: 20px; }}
        </style>
    </head>
    <body>
        <h1>PDF and DWG File Report</h1>
        <p class='summary'>{summary}</p>
    ";

            // PDF header section
            htmlContent += GenerateHeaderSection("PDF Files", pdfHeader);

            // DWG header section
            htmlContent += GenerateHeaderSection("DWG Files", dwgHeader);

            // Combined Category Counts Table
            htmlContent += @"
    <div class='section'>
        <h2>Category Counts (PDF and DWG)</h2>
        <table>
            <tr><th>Category</th><th>PDF Count</th><th>DWG Count</th></tr>
    ";

            // Build the combined table rows
            var allCategories = new HashSet<string>(pdfHeader.CategoryCounts.Keys);
            allCategories.UnionWith(dwgHeader.CategoryCounts.Keys);

            foreach (var category in allCategories)
            {
                int pdfCount = pdfHeader.CategoryCounts.ContainsKey(category) ? pdfHeader.CategoryCounts[category] : 0;
                int dwgCount = dwgHeader.CategoryCounts.ContainsKey(category) ? dwgHeader.CategoryCounts[category] : 0;
                htmlContent += $"<tr><td class='category'>{category}</td><td>{pdfCount}</td><td>{dwgCount}</td></tr>";
            }

            // Closing tags for the HTML
            htmlContent += "</table></div></body></html>";

            File.WriteAllText(htmlFilePath, htmlContent);
        }

        // Helper function to generate a summary statement
        private static string GenerateSummary(dynamic pdfHeader, dynamic dwgHeader)
        {
            return $@"
        This report provides a comprehensive overview of the PDF and DWG files within the specified directory structure,
        organized by category and pitch. The report includes {pdfHeader.PdfDrawings + dwgHeader.PdfDrawings} total files
        across {pdfHeader.TotalCategories + dwgHeader.TotalCategories} unique categories, amounting to a combined file size
        of {Math.Round(pdfHeader.TotalSizeMb + dwgHeader.TotalSizeMb, 2)} MB. The data was generated on {pdfHeader.GeneratedDate}
        and processed in {Math.Round(pdfHeader.ElapsedRuntimeSeconds, 2)} seconds, offering detailed counts of files by category
        for easy reference.
    ";
        }

        // Helper function to generate header information section
        private static string GenerateHeaderSection(string title, dynamic header)
        {
            return $@"
    <div class='section'>
        <h2>{title}</h2>
        <p><strong>Total Files:</strong> {header.PdfDrawings}</p>
        <p><strong>Total Categories:</strong> {header.TotalCategories}</p>
        <p><strong>Total Size (MB):</strong> {header.TotalSizeMb}</p>
        <p><strong>Generated Date:</strong> {header.GeneratedDate}</p>
        <p><strong>Elapsed Runtime (seconds):</strong> {header.ElapsedRuntimeSeconds}</p>
    </div>";
        }

        private static string GenerateHtmlForFileType(DirectoryStructure directoryStructure)
        {
            string BuildTree(DirectoryStructure dir, int level = 0)
            {
                var html = new System.Text.StringBuilder();
                foreach (var entry in dir)
                {
                    if (entry.Key == "files")
                    {
                        foreach (var file in (List<FileInfoModel>)entry.Value)
                        {
                            html.Append(new string(' ', level * 4));
                            html.Append($"<li><a href=\"{file.RelativePath}\" target=\"_blank\">{file.Filename}</a> ({file.SizeMb} MB)</li>");
                        }
                    }
                    else if (entry.Value is DirectoryStructure subDir)
                    {
                        html.Append(new string(' ', level * 4));
                        html.Append($"<li>{entry.Key}<ul>");
                        html.Append(BuildTree(subDir, level + 1));
                        html.Append("</ul></li>");
                    }
                }
                return html.ToString();
            }

            return $"<ul>{BuildTree(directoryStructure)}</ul>";
        }
    }
}