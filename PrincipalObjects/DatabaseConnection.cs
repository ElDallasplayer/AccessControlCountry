using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace PrincipalObjects
{
    public static class DatabaseConnection
    {
        public static string _connectionString = string.Empty;

        public static async Task SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
