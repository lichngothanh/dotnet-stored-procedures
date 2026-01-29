using System.Data;
using Infrastructure.Ado.CustomAttributes;

namespace Infrastructure.Ado.Parameters;

public class SuperHeroAssignToTeamParams
{
    [SqlParam($"@{nameof(HeroId)}", SqlDbType.UniqueIdentifier)]
    public Guid HeroId { get; set; }
    
    [SqlParam($"@{nameof(TeamId)}", SqlDbType.UniqueIdentifier)]
    public Guid? TeamId { get; init; }
}