namespace DrawingFinder.Models;

public class FileInfoModel
{
    public string Filename { get; set; }
    public string RelativePath { get; set; }
    public double SizeMb { get; set; }
    public string CreatedDate { get; set; }
}