using System.Data;
using Domain.Exceptions;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Infrastructure.Shared.Persistence;

public class SqlExecutor(IOptions<DatabaseOptions> options) : ISqlExecutor
{
    private readonly string _connectionString = options.Value.ConnectionString;

    public async Task<T> ExecuteSingleAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters,
        Func<SqlDataReader, T> mapper)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = CreateCommand(conn, storedProcedure, parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return mapper(reader);
        }

        var paramValues = string.Join(", ",
            cmd.Parameters.Cast<SqlParameter>()
                .Select(p => $"{p.ParameterName}={p.Value}"));
        throw new NotFoundException($"No record found with parameters: {paramValues}");
    }

    public async Task<IReadOnlyList<T>> ExecuteListAsync<T>(
        string storedProcedure,
        Action<SqlParameterCollection> parameters,
        Func<SqlDataReader, T> mapper)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = CreateCommand(conn, storedProcedure, parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        var results = new List<T>();

        while (await reader.ReadAsync())
        {
            results.Add(mapper(reader));
        }

        return results.AsReadOnly();
    }

    public async Task ExecuteAsync(string storedProcedure, Action<SqlParameterCollection> parameters)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = CreateCommand(conn, storedProcedure, parameters);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<T> ExecuteSingleValueAsync<T>(string storedProcedure, Action<SqlParameterCollection> parameters)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = CreateCommand(conn, storedProcedure, parameters);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return reader.GetFieldValue<T>(0);
        }

        throw new InvalidOperationException($"No scalar value returned from stored procedure '{storedProcedure}'");
    }

    private static SqlCommand CreateCommand(
        SqlConnection conn,
        string sp,
        Action<SqlParameterCollection> parameters
    )
    {
        var cmd = new SqlCommand(sp, conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        parameters.Invoke(cmd.Parameters);
        return cmd;
    }
}