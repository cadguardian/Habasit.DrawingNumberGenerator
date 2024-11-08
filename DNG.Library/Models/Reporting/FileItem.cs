public class FileItem
{
    public string Filename { get; set; } = string.Empty;
    public string RelativePath { get; set; } = string.Empty;
    public double SizeMb { get; set; }
    public DateTime CreatedDate { get; set; }
    public string FileType { get; set; } = string.Empty;
}