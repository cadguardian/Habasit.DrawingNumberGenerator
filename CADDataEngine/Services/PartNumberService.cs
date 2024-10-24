using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Client.Data;
using Microsoft.EntityFrameworkCore;
using DNG.Library.Data;

namespace Client.Services
{
    public class PartNumberService : IPartNumberService
    {
        private readonly ILogger<PartNumberService> _logger;
        private readonly PartNumberDbContext _dbContext;

        public PartNumberService(ILogger<PartNumberService> logger, PartNumberDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Initialize the service and load part numbers from the database asynchronously
        public async Task InitializeAsync()
        {
            try
            {
                // Ensure the database is created
                await _dbContext.Database.EnsureCreatedAsync();

                // Load part numbers from the database into memory (optional caching)
                var partList = await _dbContext.PartNumbers.ToListAsync();

                // Optionally, you can populate in-memory cache if needed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading part numbers from the database.");
            }
        }

        // Filter part numbers based on the drawing request and a universal search string
        public IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "")
        {
            var query = _dbContext.PartNumbers.AsQueryable();

            // Apply universal search filter if provided
            if (!string.IsNullOrWhiteSpace(universalSearch))
            {
                query = query.Where(p => p.Part.Contains(universalSearch, StringComparison.OrdinalIgnoreCase) ||
                                         p.Description.Contains(universalSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Apply additional filters based on the DrawingRequest properties
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltType))
                query = query.Where(p => p.Description.Contains(drawingRequest.BeltType, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.BeltSeries))
                query = query.Where(p => p.Description.Contains(drawingRequest.BeltSeries, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Color))
                query = query.Where(p => p.Description.Contains(drawingRequest.Color, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(drawingRequest.Material))
                query = query.Where(p => p.Description.Contains(drawingRequest.Material, StringComparison.OrdinalIgnoreCase));

            return query.ToDictionary(p => p.Part, p => p.Description);
        }
    }
}