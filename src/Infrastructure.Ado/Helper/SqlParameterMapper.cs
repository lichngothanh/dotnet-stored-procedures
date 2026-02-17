using System.Reflection;
using Infrastructure.Ado.CustomAttributes;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Ado.Helper;

public static class SqlParameterMapper
{
    public static void AddFrom<T>(
        this SqlParameterCollection parameters,
        T model
    )
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        foreach (var prop in typeof(T).GetProperties())
        {
            var attr = prop.GetCustomAttribute<SqlParamAttribute>();
            if (attr == null) continue;

            var value = prop.GetValue(model);
            var param = parameters.Add(attr.Name, attr.DbType);

            if (attr.Size > 0)
                param.Size = attr.Size;

            param.Value = value ?? DBNull.Value;
        }
    }
}

