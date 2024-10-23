using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DNG.Library.Data;
using System.Net.Http;

namespace Client.Services
{
    public class PartNumberService : IPartNumberService
    {
        private readonly ILogger<PartNumberService> _logger;
        private readonly HttpClient _httpClient;
        private Dictionary<string, string> _partNumbers = new();

        public PartNumberService(ILogger<PartNumberService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        // Initialize the service and load part numbers from a JSON file asynchronously
        public async Task InitializeAsync(string url)
        {
            try
            {
                // Use the injected HttpClient
                var response = await _httpClient.GetStringAsync(url);

                // Deserialize the JSON array into a list of PartNumber objects
                var partList = JsonSerializer.Deserialize<List<PartNumber>>(response);

                // Convert the list to a dictionary
                if (partList != null)
                {
                    _partNumbers = partList.ToDictionary(part => part.Part, part => part.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading part numbers from URL.");
            }
        }

        // Filter part numbers based on the drawing request and a universal search string
        public IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "")
        {
            var query = _partNumbers.AsEnumerable();

            // Apply universal search filter if provided
            if (!string.IsNullOrWhiteSpace(universalSearch))
            {
                query = query.Where(p => p.Key.Contains(universalSearch, StringComparison.OrdinalIgnoreCase) ||
                                         p.Value.Contains(universalSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Apply additional filters based on the DrawingRequest properties
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltType))
                query = query.Where(p => p.Value.Contains(drawingRequest.BeltType, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltSeries))
                query = query.Where(p => p.Value.Contains(drawingRequest.BeltSeries, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Color))
                query = query.Where(p => p.Value.Contains(drawingRequest.Color, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Material))
                query = query.Where(p => p.Value.Contains(drawingRequest.Material, StringComparison.OrdinalIgnoreCase));

            return query;
        }
    }

    // Define a class to represent each part number
    public class PartNumber
    {
        public string Part { get; set; }
        public string Description { get; set; }
    }
}