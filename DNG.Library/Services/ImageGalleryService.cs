using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ImageGalleryService : IImageGalleryService
{
    private readonly HttpClient _httpClient;

    public ImageGalleryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        return name.StartsWith("ModularBelt_") ? name.Substring("ModularBelt_".Length) : name;
    }
}