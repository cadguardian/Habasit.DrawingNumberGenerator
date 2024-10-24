using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Client.Data;
using DNG.Library.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace Client.Services
{
    public class PartNumberService : IPartNumberService
    {
        private readonly ILogger<PartNumberService> _logger;
        private readonly HttpClient _httpClient;
        private readonly List<PartNumber> _partNumbers;

        public PartNumberService(ILogger<PartNumberService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _partNumbers = new List<PartNumber>();
        }

        // Initialize the service and load part numbers from the CSV file asynchronously
        public async Task InitializeAsync(string csvFilePath)
        {
            try
            {
                // Use HttpClient to get the CSV file as a stream
                using var responseStream = await _httpClient.GetStreamAsync(csvFilePath);
                using var reader = new StreamReader(responseStream);

                // Read the CSV header
                var header = await reader.ReadLineAsync();

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue; // Skip empty lines

                    var values = line.Split(',');

                    // Validate column count to avoid IndexOutOfRangeException
                    if (values.Length < 2)
                    {
                        _logger.LogWarning($"Skipped a row due to insufficient columns: {line}");
                        continue;
                    }

                    // Assuming the CSV has Part and Description columns
                    var partNumber = new PartNumber
                    {
                        Part = values[0].Trim(),
                        Description = values[1].Trim()
                    };

                    _partNumbers.Add(partNumber);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading part numbers from the CSV file.");
            }
        }

        // Filter part numbers based on the drawing request and a universal search string
        public IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "")
        {
            var query = _partNumbers.AsQueryable();

            // Apply universal search filter if provided
            if (!string.IsNullOrWhiteSpace(universalSearch))
            {
                query = query.Where(p => p.Part.Contains(universalSearch, StringComparison.OrdinalIgnoreCase) ||
                                         p.Description.Contains(universalSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Apply additional filters based on the DrawingRequest properties
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltSeries))
                query = query.Where(p => p.Description.Contains(drawingRequest.BeltSeries, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Color))
                query = query.Where(p => p.Description.Contains(drawingRequest.Color, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Material))
                query = query.Where(p => p.Description.Contains(drawingRequest.Material, StringComparison.OrdinalIgnoreCase));

            return query.ToDictionary(p => p.Part, p => p.Description);
        }
    }

    public class PartNumber
    {
        public string Part { get; set; }
        public string Description { get; set; }
    }
}