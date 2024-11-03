namespace DrawingFinder.Models;

public class Header
{
    public int PdfDrawings { get; set; }
    public int TotalCategories { get; set; }
    public double TotalSizeMb { get; set; }
    public DateTime GeneratedDate { get; set; }
    public double ElapsedRuntimeSeconds { get; set; }
    public Dictionary<string, int> CategoryCounts { get; set; } = new();
}