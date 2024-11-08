using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

public class CADLibraryService : ICADLibraryService
{
    private readonly ILogger<CADLibraryService> _logger;
    private readonly string jsonPath = "data/output.json";

    public CADLibraryService(ILogger<CADLibraryService> logger)
    {
        _logger = logger;
    }

    public async Task<ReportHeader?> LoadLibraryMetadataAsync(HttpClient httpClient)
    {
        try
        {
            _logger.LogInformation("Loading report header from {JsonPath}", jsonPath);
            var stream = await httpClient.GetStreamAsync(jsonPath);
            return await JsonSerializer.DeserializeAsync<ReportHeader>(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load report header.");
            return null;
        }
    }

    public async Task<List<FileItem>> LoadLibraryFilesAsync(HttpClient httpClient)
    {
        try
        {
            var stream = await httpClient.GetStreamAsync(jsonPath);
            var jsonDoc = await JsonDocument.ParseAsync(stream);
            var files = new List<FileItem>();

            if (jsonDoc.RootElement.TryGetProperty("directory_structure", out JsonElement dirStruct))
            {
                ExtractFiles(dirStruct, files);
            }
            return files;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load files from JSON.");
            return new List<FileItem>();
        }
    }

    public List<FileItemDisplay> SearchLibraryFiles(string query, List<FileItem> allFiles)
    {
        return allFiles
            .Where(f => f.Filename.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Select(f => new FileItemDisplay
            {
                Filename = f.Filename,
                DisplayName = Path.GetFileNameWithoutExtension(f.Filename),
                RelativePath = f.RelativePath
            })
            .ToList();
    }

    private void ExtractFiles(JsonElement directory, List<FileItem> files)
    {
        foreach (var property in directory.EnumerateObject())
        {
            if (property.Name.Equals("files", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var file in property.Value.EnumerateArray())
                {
                    files.Add(new FileItem
                    {
                        Filename = file.GetProperty("filename").GetString() ?? string.Empty,
                        RelativePath = file.GetProperty("relative_path").GetString() ?? string.Empty,
                        SizeMb = file.GetProperty("size_mb").GetDouble(),
                        CreatedDate = file.GetProperty("created_date").GetDateTime()
                    });
                }
            }
            else
            {
                ExtractFiles(property.Value, files);
            }
        }
    }

    public async Task<Header> GenerateLibraryReportAsync(string targetDirectory, int categoryDepth)
    {
        _logger.LogInformation($"Starting to process directory: {targetDirectory}");

        var pdfData = new DirectoryStructure();
        var dwgData = new DirectoryStructure();
        var pdfCategories = new Dictionary<string, int>();
        var dwgCategories = new Dictionary<string, int>();
        int pdfDrawings = 0, dwgDrawings = 0;
        long pdfTotalSizeBytes = 0, dwgTotalSizeBytes = 0;

        if (!Directory.Exists(targetDirectory))
        {
            _logger.LogError($"The directory {targetDirectory} does not exist.");
            return null;
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        var files = Directory.EnumerateFiles(targetDirectory, "*.*", SearchOption.AllDirectories)
            .Where(file => file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".dwg", StringComparison.OrdinalIgnoreCase));

        Parallel.ForEach(files, file =>
        {
            ProcessFile(file, targetDirectory, pdfData, dwgData, ref pdfDrawings, ref dwgDrawings, ref pdfTotalSizeBytes, ref dwgTotalSizeBytes, pdfCategories, dwgCategories);
        });

        stopwatch.Stop();

        var pdfHeader = CreateHeader(pdfDrawings, pdfTotalSizeBytes, pdfCategories, stopwatch.Elapsed.TotalSeconds);
        var dwgHeader = CreateHeader(dwgDrawings, dwgTotalSizeBytes, dwgCategories, stopwatch.Elapsed.TotalSeconds);

        return new Header
        {
            PdfDrawings = pdfDrawings + dwgDrawings,
            TotalCategories = pdfCategories.Count + dwgCategories.Count,
            TotalSizeMb = Math.Round((double)(pdfTotalSizeBytes + dwgTotalSizeBytes) / (1024 * 1024), 2),
            GeneratedDate = DateTime.Now,
            ElapsedRuntimeSeconds = stopwatch.Elapsed.TotalSeconds,
            CategoryCounts = pdfCategories.Concat(dwgCategories).ToDictionary(pair => pair.Key, pair => pair.Value)
        };
    }

    private void ProcessFile(
        string file, string targetDirectory, DirectoryStructure pdfData, DirectoryStructure dwgData,
        ref int pdfDrawings, ref int dwgDrawings, ref long pdfTotalSizeBytes, ref long dwgTotalSizeBytes,
        Dictionary<string, int> pdfCategories, Dictionary<string, int> dwgCategories)
    {
        var fileLower = Path.GetFileName(file).ToLower();
        if (fileLower.Contains("do-not")) return;

        try
        {
            var relativePath = Path.GetRelativePath(targetDirectory, file);
            var fileInfo = new FileInfo(file);
            var sizeMb = Math.Round((double)fileInfo.Length / (1024 * 1024), 2);
            var createdDate = fileInfo.CreationTime;
            bool isPdf = fileLower.EndsWith(".pdf");

            var relativeDir = Path.GetDirectoryName(relativePath) ?? string.Empty;
            var fileModel = new FileItem
            {
                Filename = Path.GetFileName(file),
                RelativePath = relativePath.Replace("\\", "/"),
                SizeMb = sizeMb,
                CreatedDate = createdDate
            };

            if (isPdf)
            {
                System.Threading.Interlocked.Increment(ref pdfDrawings);
                System.Threading.Interlocked.Add(ref pdfTotalSizeBytes, fileInfo.Length);
                lock (pdfData)
                {
                    AddToDirectoryStructure(pdfData, relativeDir, fileModel, pdfCategories);
                }
            }
            else
            {
                System.Threading.Interlocked.Increment(ref dwgDrawings);
                System.Threading.Interlocked.Add(ref dwgTotalSizeBytes, fileInfo.Length);
                lock (dwgData)
                {
                    AddToDirectoryStructure(dwgData, relativeDir, fileModel, dwgCategories);
                }
            }

            _logger.LogInformation($"Processed file: {relativePath} | Size: {sizeMb} MB | Created Date: {createdDate:O}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing file {file}: {ex.Message}");
        }
    }

    private Header CreateHeader(int drawingCount, long totalSizeBytes, Dictionary<string, int> categories, double elapsedTimeSeconds)
    {
        return new Header
        {
            PdfDrawings = drawingCount,
            TotalCategories = categories.Count,
            TotalSizeMb = Math.Round((double)totalSizeBytes / (1024 * 1024), 2),
            GeneratedDate = DateTime.Now,
            ElapsedRuntimeSeconds = elapsedTimeSeconds,
            CategoryCounts = new Dictionary<string, int>(categories)
        };
    }

    private void AddToDirectoryStructure(
        DirectoryStructure currentLevel, string relativeDir, FileItem fileInfo,
        Dictionary<string, int> categories)
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
            currentLevel["files"] = new List<FileItem>();
        ((List<FileItem>)currentLevel["files"]).Add(fileInfo);
    }

    public async Task SaveLibraryJsonReportAsync(string filePath, Header pdfHeader, Header dwgHeader)
    {
        var outputData = new
        {
            PDF = new OutputData { Header = pdfHeader, Directory_Structure = new DirectoryStructure() },
            DWG = new OutputData { Header = dwgHeader, Directory_Structure = new DirectoryStructure() }
        };

        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(outputData, jsonOptions);
        await File.WriteAllTextAsync(filePath, jsonString);
        _logger.LogInformation("JSON report generated at: {filePath}");
    }

    public async Task SaveLibraryHtmlReportAsync(string filePath, Header pdfHeader, Header dwgHeader)
    {
        string htmlContent = GenerateHtmlContent(pdfHeader, dwgHeader);
        await File.WriteAllTextAsync(filePath, htmlContent);
        _logger.LogInformation("HTML report generated at: {filePath}");
    }

    private string GenerateHtmlContent(Header pdfHeader, Header dwgHeader)
    {
        return $@"
        <html>
        <body>
            <h1>PDF and DWG File Report</h1>
            <div>Total Files (PDF): {pdfHeader.PdfDrawings}</div>
            <div>Total Files (DWG): {dwgHeader.PdfDrawings}</div>
            <div>Total Size (PDF): {pdfHeader.TotalSizeMb} MB</div>
            <div>Total Size (DWG): {dwgHeader.TotalSizeMb} MB</div>
            <div>Generated on: {pdfHeader.GeneratedDate}</div>
        </body>
        </html>";
    }
}