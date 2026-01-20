using Domain.Common;
using Domain.SuperHeroes;
using Domain.Teams;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Ado.Mapping;

public static class SuperHeroMapper
{
    public static SuperHero MapToSuperHero(SqlDataReader reader)
    {
        var heroId = HeroId.From(reader.GetGuid(reader.GetOrdinal("HeroId")));
        var heroName = HeroName.From(reader.GetString(reader.GetOrdinal("HeroName")));
        var realName = RealName.From(reader.GetString(reader.GetOrdinal("RealName")));
        var powerLevel = PowerLevel.From(reader.GetInt32(reader.GetOrdinal("PowerLevel")));
        var universe = Universe.From(reader.GetString(reader.GetOrdinal("Universe")));

        var hero = new SuperHero(heroId, heroName, realName, powerLevel, universe);

        if (!reader.IsDBNull(reader.GetOrdinal("TeamId")))
        {
            var teamId = TeamId.From(reader.GetGuid(reader.GetOrdinal("TeamId")));
            hero.AssignToTeam(teamId);
        }

        return hero;
    }
}