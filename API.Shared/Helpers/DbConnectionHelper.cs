using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API.Shared.Helpers
{
    public class DbConnectionHelper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("APICon")!;
        }
        public IDbConnection CreateConnection()
           => new NpgsqlConnection(_connectionString);
    }
}