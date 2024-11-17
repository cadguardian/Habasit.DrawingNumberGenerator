using DNG.Library.Models;
using DNG.Library.Models.Base;
using DNG.Library.Services.Base;
using Microsoft.Extensions.Logging;
using System.Text;

/// <summary>
/// Service responsible for loading and filtering part numbers.
/// </summary>
public class PartNumberService : IPartNumberService
{
    private readonly ILogger<PartNumberService> _logger;
    private readonly HttpClient _httpClient;
    private readonly List<PartNumber> _parts;

    public List<PartNumber> GetParts()
    {
        return _parts;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PartNumberService"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="httpClient">The HTTP client for fetching CSV data.</param>
    public PartNumberService(ILogger<PartNumberService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _parts = new List<PartNumber>();
    }

    /// <summary>
    /// Initializes the service and loads part numbers from the CSV file asynchronously.
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            var csvFilePath = "https://raw.githubusercontent.com/cadguardian/Habasit-CADMedia/refs/heads/main/partNumbers.csv";

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
                    _logger.LogDebug("Skipped a row containing '}': {Line}", line);
                    continue;
                }

                var values = SplitCsvLine(line);

                // Validate column count to avoid IndexOutOfRangeException
                if (values.Length < 2)
                {
                    _logger.LogDebug("Skipped a row due to insufficient columns: {Line}", line);
                    continue;
                }

                // Assuming the CSV has Part and Description columns
                var partNumber = new PartNumber
                {
                    Part = values[0].Trim(),
                    Description = values[1].Trim()
                };

                _parts.Add(partNumber);
            }

            _logger.LogInformation("Successfully loaded {_Count} part numbers from CSV.", _parts.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading part numbers from the CSV file.");
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

    /// <summary>
    /// Filters part numbers based on user input using standard LINQ queries.
    /// Supports two modes:
    /// - Free Search Mode: Searches for parts containing the universal search string in either the Part or Description.
    /// - Drawing Request Filter Mode: Applies specific filters based on BeltSeries, Color, and Material.
    /// </summary>
    /// <param name="drawingRequest">The drawing request containing specific filter criteria.</param>
    /// <param name="universalSearch">The universal search string for free text searching.</param>
    /// <param name="isFreeSearchMode">Determines whether to apply free search filtering or drawing request filters.</param>
    /// <returns>An enumerable of filtered part numbers and their descriptions.</returns>
    public IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "", bool isFreeSearchMode = false)
    {
        IEnumerable<PartNumber> query = _parts;

        if (isFreeSearchMode && !string.IsNullOrWhiteSpace(universalSearch))
        {
            string searchUpper = universalSearch.ToUpperInvariant();

            // Split the universal search into terms separated by spaces
            var searchTerms = searchUpper.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Apply each search term as a filter (all terms must be present)
            foreach (var term in searchTerms)
            {
                query = query.Where(p => p.Part.ToUpperInvariant().Contains(term) ||
                                         p.Description.ToUpperInvariant().Contains(term));
            }
        }

        if (!isFreeSearchMode && drawingRequest != null)
        {
            // Apply Drawing Request Filters

            // Filter by Belt Series if specified
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltSeries))
            {
                string beltSeriesUpper = drawingRequest.BeltSeries.ToUpperInvariant();
                query = query.Where(p => p.Description.ToUpperInvariant().Contains(beltSeriesUpper));
            }

            // Filter by Color if specified
            if (!string.IsNullOrWhiteSpace(drawingRequest.Color))
            {
                string colorUpper = drawingRequest.Color.ToUpperInvariant();
                query = query.Where(p => p.Description.ToUpperInvariant().Contains(colorUpper));
            }

            // Filter by Material if specified
            if (!string.IsNullOrWhiteSpace(drawingRequest.Material))
            {
                string materialUpper = drawingRequest.Material.ToUpperInvariant();
                query = query.Where(p => p.Description.ToUpperInvariant().Contains(materialUpper));
            }
        }

        return query.Select(p => new KeyValuePair<string, string>(p.Part, p.Description));
    }
}