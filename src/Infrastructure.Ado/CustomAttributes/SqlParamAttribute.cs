using System.Data;

namespace Infrastructure.Ado.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class SqlParamAttribute : Attribute
{
    public string Name { get; }
    public SqlDbType DbType { get; }
    public int Size { get; }

    public SqlParamAttribute(
        string name,
        SqlDbType dbType,
        int size
    )
    {
        Name = name;
        DbType = dbType;
        Size = size;
    }
    
    public SqlParamAttribute(
        string name,
        SqlDbType dbType
    )
    {
        Name = name;
        DbType = dbType;
    }
}

