using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Client.Data;
using DNG.Library.Data;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;

namespace Client.Services
{
    /// <summary>
    /// Service responsible for loading and filtering part numbers.
    /// </summary>
    public class PartNumberService : IPartNumberService
    {
        private readonly ILogger<PartNumberService> _logger;
        private readonly HttpClient _httpClient;
        private readonly List<PartNumber> _partNumbers;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartNumberService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="httpClient">The HTTP client for fetching CSV data.</param>
        public PartNumberService(ILogger<PartNumberService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _partNumbers = new List<PartNumber>();
        }

        /// <summary>
        /// Initializes the service and loads part numbers from the CSV file asynchronously.
        /// </summary>
        /// <param name="csvFilePath">The path to the CSV file containing part numbers.</param>
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

                    // Skip rows containing '}'
                    if (line.Contains("}"))
                    {
                        _logger.LogWarning($"Skipped a row containing '}}': {line}");
                        continue;
                    }

                    var values = SplitCsvLine(line);

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
        /// Filters part numbers based on user input without relying on wildcards or commas.
        /// Users can input multiple search terms separated by spaces, and only parts containing all terms
        /// in any order within the Part or Description fields are returned.
        /// </summary>
        /// <param name="drawingRequest">The drawing request containing additional filters.</param>
        /// <param name="universalSearch">The universal search string with multiple search terms separated by spaces.</param>
        /// <returns>An enumerable of filtered part numbers and their descriptions.</returns>
        public IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "", bool isFreeSearchMode = false)
        {
            IEnumerable<PartNumber> query = _partNumbers;

            if (!string.IsNullOrWhiteSpace(universalSearch))
            {
                var terms = universalSearch.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                           .Select(term => term.Trim())
                                           .Where(term => !string.IsNullOrEmpty(term))
                                           .ToList();

                if (terms.Any())
                {
                    foreach (var term in terms)
                    {
                        query = query.Where(p => p.Part.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                 p.Description.Contains(term, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }

            if (!isFreeSearchMode) // Skip these filters if Free Search Mode is enabled
            {
                if (!string.IsNullOrWhiteSpace(drawingRequest.BeltSeries))
                {
                    query = query.Where(p => p.Description.Contains(drawingRequest.BeltSeries, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(drawingRequest.Color))
                {
                    query = query.Where(p => p.Description.Contains(drawingRequest.Color, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(drawingRequest.Material))
                {
                    query = query.Where(p => p.Description.Contains(drawingRequest.Material, StringComparison.OrdinalIgnoreCase));
                }
            }

            return query.Select(p => new KeyValuePair<string, string>(p.Part, p.Description));
        }

        /// <summary>
        /// Represents a part number with its description.
        /// </summary>
        public class PartNumber
        {
            public string Part { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }
    }
}