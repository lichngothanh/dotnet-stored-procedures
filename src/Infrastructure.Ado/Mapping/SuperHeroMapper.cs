using Domain.Common;
using Domain.SuperHeroes;
using Domain.Teams;
using Microsoft.Data.SqlClient;
using static Application.Extension.SqlReaderMappingExtension;
namespace Infrastructure.Ado.Mapping;

public static class SuperHeroMapper
{
    public static SuperHero MapToSuperHero(SqlDataReader reader)
    {
        var heroId = HeroId.From(GetGuidSafe(reader, nameof(HeroId)));
        var heroName = HeroName.From(GetStringSafe(reader, nameof(HeroName)));
        var realName = RealName.From(GetStringSafe(reader, nameof(RealName)));
        var powerLevel = PowerLevel.From(GetIntSafe(reader, nameof(PowerLevel)));
        var universe = Universe.From(GetStringSafe(reader, nameof(Universe)));

        var hero = new SuperHero(heroId, heroName, realName, powerLevel, universe);

        if (!reader.IsDBNull(reader.GetOrdinal("TeamId")))
        {
            var teamId = TeamId.From(GetGuidSafe(reader, nameof(TeamId)));
            hero.AssignToTeam(teamId);
        }

        return hero;
    }
}