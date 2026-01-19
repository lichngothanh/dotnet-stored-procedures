using Microsoft.Data.SqlClient;

namespace Infrastructure.Shared.Abstractions;

public interface ISqlExecutor
{
    Task<T> ExecuteSingleAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters,
        Func<SqlDataReader, T> mapper);
}
