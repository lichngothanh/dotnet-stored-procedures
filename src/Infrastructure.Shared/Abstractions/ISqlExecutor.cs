using Microsoft.Data.SqlClient;

namespace Infrastructure.Shared.Abstractions;

public interface ISqlExecutor
{
    Task<T> ExecuteSingleAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters,
        Func<SqlDataReader, T> mapper);
    
    Task<IReadOnlyList<T>> ExecuteListAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters,
        Func<SqlDataReader, T> mapper);
    
    Task ExecuteAsync(
        string storedProcedure,
        Action<SqlParameterCollection> parameters);
    
    Task<T> ExecuteSingleValueAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters);
}
