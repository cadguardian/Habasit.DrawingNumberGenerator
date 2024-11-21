using DNG.Library.Services.Base;
using System.Net.Http.Json;

namespace DNG.Library.Services;

public class ImageGalleryService : IImageGalleryService
{
    private readonly HttpClient _httpClient;

    public ImageGalleryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string RemoveTrailing01(string input)
    {
        return input.EndsWith("01") ? input.Substring(0, input.Length - 2) : input;
    }

    public async Task<List<string>> LoadImageFilesAsync()
    {
        var imageFiles = await _httpClient.GetFromJsonAsync<List<string>>("images/belts/images.json") ?? new List<string>();

        Console.WriteLine($"{imageFiles!.Count} belt images found.");

        return imageFiles;
    }

    public IEnumerable<string> FilterImages(List<string> images, string searchQuery)
    {
        return images.Where(img => img.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
    }

    public string FormatImageName(string imageName)
    {
        string name = Path.GetFileNameWithoutExtension(imageName).Replace("_", " ");

        name = RemoveTrailing01(name);

        return name.StartsWith("ModularBelt ") ? name.Substring("ModularBelt ".Length) : name;
    }
}