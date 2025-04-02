using CADCleanser;

public class FileProcessor : IFileProcessor
{
    private readonly ISolidWorksService _solidWorksService;
    private readonly ReportGenerator _reportGenerator;

    public FileProcessor(ISolidWorksService solidWorksService, ReportGenerator reportGenerator)
    {
        _solidWorksService = solidWorksService;
        _reportGenerator = reportGenerator;
    }

    public void ProcessFile(string filePath)
    {
        //var errors = _solidWorksService.GetDocumentErrors(filePath);
        // string status = errors.Count == 0 ? "No Issues" : string.Join("; ", errors);

        var report = new FileReport
        {
            FileName = Path.GetFileName(filePath),
            FilePath = filePath,
        };

        _reportGenerator.AddFileReport(report);
    }
}