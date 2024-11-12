using Client.Data;
using DNG.Library.Data;
using DNG.Library.Services.Base;
using Microsoft.Extensions.Logging;
using System.Text;

public class RequiredBeltWidth
{
    public string Pitch { get; set; } = string.Empty;
    public double RequiredWidth { get; set; }
}

/// <summary>
/// Service responsible for loading and filtering part numbers.
/// </summary>
public class RequiredBeltWidthService : IRequiredBeltWidthService
{
    private readonly ILogger<RequiredBeltWidthService> _logger;
    private readonly HttpClient _httpClient;
    private readonly List<RequiredBeltWidth> _requiredBeltWidthStandards;

    public List<RequiredBeltWidth> GetRequiredBeltWidthStandards()
    {
        return _requiredBeltWidthStandards;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequiredBeltWidthService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="httpClient">The HTTP client for fetching CSV data.</param>
    public RequiredBeltWidthService(ILogger<RequiredBeltWidthService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _requiredBeltWidthStandards = new List<RequiredBeltWidth>();
    }

    /// <summary>
    /// Initializes the service and loads part numbers from the CSV file asynchronously.
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            var csvFilePath = "https://raw.githubusercontent.com/cadguardian/Habasit-CADMedia/refs/heads/main/RequiredBeltWidthBySeriesAndPitch.csv";

            // Use HttpClient to get the CSV file as a stream
            using var responseStream = await _httpClient.GetStreamAsync(csvFilePath);
            using var reader = new StreamReader(responseStream);

            // Read the CSV header
            var header = await reader.ReadLineAsync();
            _logger.LogInformation("CSV Header: {Header}", header);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines

                // Skip rows containing '}'
                if (line.Contains("}"))
                {
                    _logger.LogWarning("Skipped a row containing '}': {Line}", line);
                    continue;
                }

                var values = SplitCsvLine(line);

                // Validate column count to avoid IndexOutOfRangeException
                if (values.Length < 2)
                {
                    _logger.LogWarning("Skipped a row due to insufficient columns: {Line}", line);
                    continue;
                }

                double.TryParse(values[1].Trim(), out double width);

                // Assuming the CSV has Part and Description columns
                var standardBeltWidth = new RequiredBeltWidth
                {
                    Pitch = values[0].Trim(),
                    RequiredWidth = width,
                };

                _requiredBeltWidthStandards.Add(standardBeltWidth);
            }

            _logger.LogInformation("Successfully loaded {_Count} required belt width standards from CSV.", _requiredBeltWidthStandards.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading required belt width standards from the CSV file.");
        }
    }

    /// <summary>
    /// Splits a CSV line into an array of values, handling quoted commas.
    /// </summary>
    /// <param name="line">The CSV line to split.</param>
    /// <returns>An array of values.</returns>
    private string[] SplitCsvLine(string line)
    {
        var values = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;

        foreach (var ch in line)
        {
            if (ch == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (ch == ',' && !inQuotes)
            {
                values.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(ch);
            }
        }

        values.Add(current.ToString());

        return values.ToArray();
    }
}