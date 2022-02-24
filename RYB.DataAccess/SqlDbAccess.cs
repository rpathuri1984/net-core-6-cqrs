using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RYB.DataAccess
{
    public class SqlDbAccess : IDbAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDbAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U parameters,
            CommandType commandType= CommandType.Text,
            string connectionId = "Default"
            )
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: commandType);
        }

        public async Task SavaData<T>(string storedProcedure,
            T parameters,
            CommandType commandType = CommandType.Text,
            string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: commandType);
        }
    }
}