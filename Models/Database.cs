using System;
using Microsoft.Data.SqlClient; // Correct namespace for Microsoft.Data.SqlClient
using Microsoft.Extensions.Configuration;

namespace CadastroWebApp.Models
{
    public class Database(IConfiguration configuration)
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("DefaultConnection is not configured.");

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString); // Uses Microsoft.Data.SqlClient.SqlConnection
        }
    }
}