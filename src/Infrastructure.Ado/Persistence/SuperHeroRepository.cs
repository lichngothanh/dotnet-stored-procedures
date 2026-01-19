using Domain.Common;
using Domain.SuperHeroes;
using Domain.Teams;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Constants;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Ado.Persistence;

public class SuperHeroRepository : ISuperHeroRepository
{

    private readonly ISqlExecutor _sql;
    public SuperHeroRepository(ISqlExecutor sql)
    {
        _sql = sql;
    }
    private static SuperHero MapToSuperHero(SqlDataReader reader)
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

    public Task AddAsync(SuperHero hero)
    {
        throw new NotImplementedException();
    }

    public Task AssignToTeamAsync(HeroId heroId, TeamId teamId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SuperHero>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SuperHero> GetByIdAsync(HeroId heroId)
    {
        return
            _sql.ExecuteSingleAsync(
                StoredProcedures.SuperHero.GetById,
                x => x.Add("@HeroId", SqlDbType.UniqueIdentifier).Value = heroId.Value,
                MapToSuperHero
            );
    }

    public Task UpdateAsync(SuperHero hero)
    {
        throw new NotImplementedException();
    }
}