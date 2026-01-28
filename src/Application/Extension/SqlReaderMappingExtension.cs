using Microsoft.Data.SqlClient;

namespace Application.Extension;

public static class SqlReaderMappingExtension
{
    public static string GetStringSafe(SqlDataReader reader, string columnName)
    {
        return reader.GetString(reader.GetOrdinal(columnName));
    }
    
    public static Guid GetGuidSafe(SqlDataReader reader, string columnName)
    {
        return reader.GetGuid(reader.GetOrdinal(columnName));
    }
    
    public static int GetIntSafe(SqlDataReader reader, string columnName)
    {
        return reader.GetInt32(reader.GetOrdinal(columnName));
    }
}