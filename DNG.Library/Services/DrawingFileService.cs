using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.JSInterop;

public class DrawingFileService : IDrawingFileService
{
    public async Task<List<FileItem>> LoadAllFilesAsync(HttpClient httpClient, string jsonPath)
    {
        var allFiles = new List<FileItem>();

        try
        {
            var stream = await httpClient.GetStreamAsync(jsonPath);
            var jsonDoc = await JsonDocument.ParseAsync(stream);

            if (jsonDoc.RootElement.TryGetProperty("directory_structure", out JsonElement directoryStructure))
            {
                ExtractFiles(directoryStructure, "", allFiles);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing JSON: {ex.Message}");
        }

        return allFiles;
    }

    public async Task<ReportHeader?> LoadReportHeaderAsync(HttpClient httpClient, string jsonPath)
    {
        try
        {
            var stream = await httpClient.GetStreamAsync(jsonPath);
            var jsonDoc = await JsonDocument.ParseAsync(stream);

            if (jsonDoc.RootElement.TryGetProperty("header", out JsonElement headerElement))
            {
                return JsonSerializer.Deserialize<ReportHeader>(headerElement.GetRawText());
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing JSON header: {ex.Message}");
        }

        return null;
    }

    private void ExtractFiles(JsonElement directory, string currentCategory, List<FileItem> files)
    {
        foreach (var property in directory.EnumerateObject())
        {
            if (property.Name.Equals("files", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var file in property.Value.EnumerateArray())
                {
                    var fullpath = @"K:\Operations\Modular\Special Builds\Belts\" + file.GetProperty("relative_path")!.GetString()!;

                    files.Add(new FileItem
                    {
                        Filename = file.GetProperty("filename").GetString() ?? string.Empty,
                        RelativePath = fullpath,
                        SizeMb = file.GetProperty("size_mb").GetDouble(),
                        CreatedDate = file.GetProperty("created_date").GetDateTime(),
                        FileType = file.GetProperty("filename").GetString()?.EndsWith(".pdf") == true ? "PDF" : "DWG"
                    });
                }
            }
            else
            {
                ExtractFiles(property.Value, property.Name, files);
            }
        }
    }

    public List<FileItemDisplay> SearchFiles(string searchQuery, List<FileItem> allFiles, string drawingNumberQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return new List<FileItemDisplay>();

        var query = searchQuery.ToLower();
        var regexPattern = "^" + Regex.Escape(searchQuery).Replace("\\*", ".*") + "$";
        var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

        return allFiles
            .Where(f => string.IsNullOrWhiteSpace(drawingNumberQuery) || regex.IsMatch(f.Filename))
            .Select(f => new FileItemDisplay
            {
                Filename = f.Filename,
                DisplayName = GetHumanReadableName(f.Filename),
                RelativePath = f.RelativePath,
                FileType = f.FileType
            })
            .ToList();
    }

    public string GenerateUniversalSearch(string drawingNumberQuery)
    {
        return string.IsNullOrWhiteSpace(drawingNumberQuery) ? string.Empty : drawingNumberQuery;
    }

    public async Task CopyFilePathToClipboard(IJSRuntime js, string filePath)
    {
        await js.InvokeVoidAsync("navigator.clipboard.writeText", filePath);
        await js.InvokeVoidAsync("showToast", filePath);
    }

    private string GetHumanReadableName(string filename)
    {
        return Path.GetFileNameWithoutExtension(filename);
    }
}