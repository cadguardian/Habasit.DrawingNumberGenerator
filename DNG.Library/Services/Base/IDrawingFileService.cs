﻿using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDrawingFileService
{
    Task<List<FileItem>> LoadAllFilesAsync(HttpClient httpClient, string jsonPath);

    Task<ReportHeader?> LoadReportHeaderAsync(HttpClient httpClient, string jsonPath);

    List<FileItemDisplay> SearchFiles(string searchQuery, List<FileItem> allFiles, string drawingNumberQuery);

    string GenerateUniversalSearch(string drawingNumberQuery);

    Task CopyFilePathToClipboard(IJSRuntime js, string filePath);
}