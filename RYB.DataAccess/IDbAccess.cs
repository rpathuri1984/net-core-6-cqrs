
using System.Data;

namespace RYB.DataAccess
{
    public interface IDbAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters,
            CommandType commandType = CommandType.Text, string connectionId = "Default");
        Task SavaData<T>(string storedProcedure, T parameters,
            CommandType commandType = CommandType.Text, string connectionId = "Default");
    }
}