using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

public class PartsListService : IPartsListService
{
    private readonly HttpClient httpClient;
    private Dictionary<string, string> partsDictionary;

    public PartsListService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        //LoadPartsAsync();
    }

    private async void LoadPartsAsync()
    {
        // Specify the relative path to the CSV file without 'wwwroot'
        var filePath = "data/all-purch-parts-2023-abridged.csv";

        try
        {
            // Use HttpClient to read the file
            var fileContent = await httpClient.GetStringAsync(filePath);
            var lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Check if the CSV has more than just the header
            if (lines.Length <= 1)
            {
                Console.WriteLine("CSV file is empty or only contains the header.");
                partsDictionary = new Dictionary<string, string>(); // Initialize to prevent null reference
                return;
            }

            partsDictionary = lines
                .Skip(1) // Skip header
                .Select(line => line.Split(','))
                .Where(parts => parts.Length >= 2) // Ensure there are at least two parts
                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

            Console.WriteLine($"CSV file loaded successfully: {filePath}");
            Console.WriteLine($"Number of entries found: {partsDictionary.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading parts: {ex.Message}");
            partsDictionary = new Dictionary<string, string>(); // Initialize to prevent null reference
        }
    }

    public IEnumerable<KeyValuePair<string, string>> FilterParts(string beltType, string beltSeries, string color, string material)
    {
        return partsDictionary
            .Where(part =>
                (string.IsNullOrEmpty(beltType) || part.Value.Contains(beltType, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(beltSeries) || part.Value.Contains(beltSeries, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(color) || part.Value.Contains(color, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(material) || part.Value.Contains(material, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }
}