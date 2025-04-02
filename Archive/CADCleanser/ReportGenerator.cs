using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADCleanser
{
    public class ReportGenerator
    {
        private readonly List<FileReport> _fileReports = new();

        public void AddFileReport(FileReport report)
        {
            _fileReports.Add(report);
        }

        public void GenerateHtmlReport(string outputPath)
        {
            var html = new StringBuilder();
            html.Append("<html><body>");
            foreach (var report in _fileReports)
            {
                html.Append($"<div><h3>{report.FileName}</h3>");
                html.Append($"<p>Path: {report.FilePath}</p>");
                html.Append($"<p>Status: {report.Status}</p>");
                html.Append("</div>");
            }
            html.Append("</body></html>");
            File.WriteAllText(outputPath, html.ToString());
        }
    }

    public class FileReport
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        // Additional properties as needed
    }
}