public interface ICADLibraryService
{
    /// <summary>
    /// Loads metadata for the CAD library from a specified JSON file.
    /// </summary>
    /// <param name="httpClient">The HttpClient used to fetch the JSON file.</param>
    /// <returns>A ReportHeader object containing metadata information or null if loading fails.</returns>
    Task<ReportHeader?> LoadLibraryMetadataAsync(HttpClient httpClient);

    /// <summary>
    /// Processes a specified directory to categorize and aggregate CAD files, generating a structured report.
    /// </summary>
    /// <param name="targetDirectory">The directory path containing CAD files to process.</param>
    /// <param name="categoryDepth">The depth level for organizing files into categories.</param>
    /// <returns>A Header object with aggregate information about processed files.</returns>
    Task<Header> GenerateLibraryReportAsync(string targetDirectory, int categoryDepth);

    /// <summary>
    /// Saves a generated report of the CAD library as a JSON file.
    /// </summary>
    /// <param name="filePath">The file path where the JSON report will be saved.</param>
    /// <param name="pdfHeader">Header data summarizing PDF files in the library.</param>
    /// <param name="dwgHeader">Header data summarizing DWG files in the library.</param>
    Task SaveLibraryJsonReportAsync(string filePath, Header pdfHeader, Header dwgHeader);

    /// <summary>
    /// Saves a generated report of the CAD library as an HTML file for easy viewing.
    /// </summary>
    /// <param name="filePath">The file path where the HTML report will be saved.</param>
    /// <param name="pdfHeader">Header data summarizing PDF files in the library.</param>
    /// <param name="dwgHeader">Header data summarizing DWG files in the library.</param>
    Task SaveLibraryHtmlReportAsync(string filePath, Header pdfHeader, Header dwgHeader);

    /// <summary>
    /// Searches for files in the CAD library based on a query string.
    /// </summary>
    /// <param name="query">The search query to filter files.</param>
    /// <param name="allFiles">A list of all files available in the library.</param>
    /// <returns>A list of FileItemDisplay objects matching the search criteria.</returns>
    List<FileItemDisplay> SearchLibraryFiles(string query, List<FileItem> allFiles);

    /// <summary>
    /// Loads all files from a specified JSON file in the CAD library, creating a list of file items.
    /// </summary>
    /// <param name="httpClient">The HttpClient used to fetch the JSON file.</param>
    /// <returns>A list of FileItem objects representing files in the library.</returns>
    Task<List<FileItem>> LoadLibraryFilesAsync(HttpClient httpClient);
}