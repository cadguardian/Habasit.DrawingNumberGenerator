using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DNG.Library.Models;
using DNG.Library.Services.Base;

namespace DNG.Library.Services
{
    public class DrawingFilterService : IDrawingFilterService
    {
        private readonly string _connectionString;

        public DrawingFilterService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SQLiteConnection(_connectionString);

        // 🔹 1️⃣ Single Attribute Filter (Exact Match)
        public async Task<IEnumerable<DrawingNumber>> FilterByAttributeAsync(string attribute, string value)
        {
            using var db = CreateConnection();
            string sql = $"SELECT * FROM Drawings WHERE {attribute} = @Value";
            return await db.QueryAsync<DrawingNumber>(sql, new { Value = value });
        }

        // 🔹 2️⃣ Numeric Range Filter (Min/Max)
        public async Task<IEnumerable<DrawingNumber>> FilterByRangeAsync(string attribute, double min, double max)
        {
            using var db = CreateConnection();
            string sql = $"SELECT * FROM Drawings WHERE {attribute} BETWEEN @Min AND @Max";
            return await db.QueryAsync<DrawingNumber>(sql, new { Min = min, Max = max });
        }

        // 🔹 3️⃣ Multi-Filter Query (Multiple Attributes + Ranges)
        public async Task<IEnumerable<DrawingNumber>> MultiFilterAsync(Dictionary<string, object> filters)
        {
            using var db = CreateConnection();
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            foreach (var filter in filters)
            {
                if (filter.Value is string)
                {
                    conditions.Add($"{filter.Key} = @{filter.Key}");
                    parameters.Add(filter.Key, filter.Value);
                }
                else if (filter.Value is (double min, double max))
                {
                    conditions.Add($"{filter.Key} BETWEEN @Min_{filter.Key} AND @Max_{filter.Key}");
                    parameters.Add($"Min_{filter.Key}", min);
                    parameters.Add($"Max_{filter.Key}", max);
                }
            }

            string sql = $"SELECT * FROM Drawings WHERE {string.Join(" AND ", conditions)}";
            return await db.QueryAsync<DrawingNumber>(sql, parameters);
        }

        // 🔹 4️⃣ Get Available Values for Filtering (Dropdown List)
        public async Task<IEnumerable<string>> GetAvailableValuesAsync(string attribute)
        {
            using var db = CreateConnection();
            string sql = $"SELECT DISTINCT {attribute} FROM Drawings ORDER BY {attribute}";
            return await db.QueryAsync<string>(sql);
        }
    }
}
