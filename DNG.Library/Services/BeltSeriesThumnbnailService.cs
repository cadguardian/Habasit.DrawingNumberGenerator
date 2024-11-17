using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

public class BeltSeriesThumnbnailService : IBeltSeriesService
{
    private readonly ILogger<BeltSeriesThumnbnailService> _logger;

    public BeltSeriesThumnbnailService(ILogger<BeltSeriesThumnbnailService> logger)
    {
        _logger = logger;
    }

    // Loads image file names from a specified path asynchronously
    public async Task<List<string>> LoadImageFilesAsync(HttpClient httpClient)
    {
        string imagePath = "images/belts/images.json";

        try
        {
            _logger.LogInformation("Loading image files from path: {ImagePath}", imagePath);
            var imageFiles = await httpClient.GetFromJsonAsync<List<string>>(imagePath);
            return imageFiles ?? new List<string>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load image files from {ImagePath}", imagePath);
            return new List<string>();
        }
    }

    // Matches the best image file name for a given belt series code
    public string? GetImageFileNameForBeltSeries(string beltSeriesCode, List<string> imageFileNames)
    {
        _logger.LogInformation("Finding image for belt series: {BeltSeriesCode}", beltSeriesCode);

        var beltTokens = Tokenize(beltSeriesCode);
        int maxMatches = 0;
        string? bestMatch = null;

        foreach (var imageFile in imageFileNames)
        {
            var imageName = NormalizeImageName(imageFile);
            var imageTokens = Tokenize(imageName);
            int matches = beltTokens.Intersect(imageTokens).Count();

            _logger.LogDebug("Matching {ImageFile}: {MatchCount} matches", imageFile, matches);

            if (matches > maxMatches)
            {
                maxMatches = matches;
                bestMatch = imageFile;
            }
        }

        _logger.LogInformation("Best match for {BeltSeriesCode}: {BestMatch}", beltSeriesCode, bestMatch);
        return bestMatch;
    }

    // Determines the belt series type (can be extended to handle types logic)
    public string GetBeltSeriesType(string beltSeriesCode)
    {
        // Mock example, implement as needed
        return string.IsNullOrWhiteSpace(beltSeriesCode) ? "Unknown Series" : beltSeriesCode;
    }

    // Tokenizes a given string into alphabetical and numeric parts
    private static List<string> Tokenize(string input)
    {
        var matches = Regex.Matches(input ?? string.Empty, @"[A-Za-z]+|\d+");
        return matches.Select(m => m.Value.ToUpper()).ToList();
    }

    // Normalizes the image name by stripping unnecessary prefixes and converting to uppercase
    private static string NormalizeImageName(string imageName)
    {
        string name = Path.GetFileNameWithoutExtension(imageName ?? string.Empty);
        if (name.StartsWith("ModularBelt_", StringComparison.OrdinalIgnoreCase))
        {
            name = name.Substring("ModularBelt_".Length);
        }
        return name.ToUpper();
    }

    public string RemoveTrailing01(string input)
    {
        return input.EndsWith("01") ? input.Substring(0, input.Length - 2) : input;
    }
}