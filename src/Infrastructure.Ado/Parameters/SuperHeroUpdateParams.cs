using System.Data;
using Infrastructure.Ado.CustomAttributes;

namespace Infrastructure.Ado.Parameters;

public class SuperHeroUpdateParams
{
    [SqlParam($"@{nameof(HeroId)}", SqlDbType.UniqueIdentifier)]
    public Guid HeroId { get; set; }
    
    [SqlParam($"@{nameof(HeroName)}", SqlDbType.NVarChar, 100)]
    public string HeroName { get; init; } = default!;

    [SqlParam($"@{nameof(RealName)}", SqlDbType.NVarChar, 150)]
    public string RealName { get; init; } = default!;

    [SqlParam($"@{nameof(PowerLevel)}", SqlDbType.Int)]
    public int PowerLevel { get; init; }

    [SqlParam($"@{nameof(Universe)}", SqlDbType.NVarChar, 20)]
    public string Universe { get; init; } = default!;
}