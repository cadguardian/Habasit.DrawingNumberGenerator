namespace DNG.Library.Services.Base;

public interface IImageGalleryService
{
    IEnumerable<string> FilterImages(List<string> images, string searchQuery);

    string FormatImageName(string imageName);

    Task<List<string>> LoadImageFilesAsync();

    string RemoveTrailing01(string input);
}