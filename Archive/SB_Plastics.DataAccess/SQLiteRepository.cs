using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SB_Plastics.DataAccess
{
    public class SQLiteRepository<T> : IRepository<T>
    {
        private readonly DatabaseContext _dbContext;
        private readonly string _tableName;

        public SQLiteRepository(DatabaseContext dbContext, string tableName)
        {
            _dbContext = dbContext;
            _tableName = tableName;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var db = _dbContext.GetConnection();
            string sql = $"SELECT * FROM {_tableName}";
            return await db.QueryAsync<T>(sql);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            using var db = _dbContext.GetConnection();
            string sql = $"SELECT * FROM {_tableName} WHERE ID = @Id";
            return await db.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<int> InsertAsync(T entity)
        {
            using var db = _dbContext.GetConnection();
            string sql = GenerateInsertQuery();
            return await db.ExecuteAsync(sql, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            using var db = _dbContext.GetConnection();
            string sql = GenerateUpdateQuery();
            return await db.ExecuteAsync(sql, entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var db = _dbContext.GetConnection();
            string sql = $"DELETE FROM {_tableName} WHERE ID = @Id";
            return await db.ExecuteAsync(sql, new { Id = id });
        }

        private string GenerateInsertQuery()
        {
            // 🔹 Placeholder for auto-generating SQL INSERT statements based on T
            return $"INSERT INTO {_tableName} (...) VALUES (...)";
        }

        private string GenerateUpdateQuery()
        {
            // 🔹 Placeholder for auto-generating SQL UPDATE statements based on T
            return $"UPDATE {_tableName} SET ... WHERE ID = @Id";
        }
    }
}