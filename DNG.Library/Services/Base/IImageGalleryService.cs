
public interface IImageGalleryService
{
    IEnumerable<string> FilterImages(List<string> images, string searchQuery);
    string FormatImageName(string imageName);
    Task<List<string>> LoadImageFilesAsync();
}