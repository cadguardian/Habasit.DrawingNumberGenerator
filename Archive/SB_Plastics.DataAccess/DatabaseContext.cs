using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SB_Plastics.DataAccess
{
    public class DatabaseContext : IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _dbConnection;

        public DatabaseContext(string databasePath)
        {
            // 🔹 Connection string pointing to the SQLite file (deployed in the client app)
            _connectionString = $"Data Source={databasePath};Version=3;";
            _dbConnection = new SQLiteConnection(_connectionString);
        }

        // 🔹 Get Database Connection
        public IDbConnection GetConnection()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            return _dbConnection;
        }

        public void Dispose()
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
        }
    }
}