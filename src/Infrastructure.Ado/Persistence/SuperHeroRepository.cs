using Domain.SuperHeroes;
using Domain.Teams;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Constants;
using System.Data;
using Infrastructure.Ado.Helper;
using Infrastructure.Ado.Parameters;
using static Infrastructure.Ado.Mapping.SuperHeroMapper;
namespace Infrastructure.Ado.Persistence;

public class SuperHeroRepository(ISqlExecutor sql) : ISuperHeroRepository
{
    public async Task<Guid> AddAsync(SuperHero hero)
    {
        var heroId = await sql.ExecuteSingleValueAsync<Guid>(
            StoredProcedures.SuperHero.Insert,
            x => x.AddFrom(new SuperHeroInsertParams
            {
                HeroName = hero.HeroName.ToString(),
                RealName = hero.RealName.ToString(),
                PowerLevel = hero.PowerLevel.Value,
                Universe = hero.Universe.ToString(),
                TeamId = hero.TeamId?.Value
            })
        );

        return heroId;
    }

    public async Task AssignToTeamAsync(HeroId heroId, TeamId teamId)
    {
        await sql.ExecuteAsync(
            StoredProcedures.SuperHero.AssignToTeam,
            x => x.AddFrom(new SuperHeroAssignToTeamParams()
            {
                HeroId = heroId.Value,
                TeamId = teamId.Value
            })
        );
    }

    public Task<IReadOnlyList<SuperHero>> GetAllAsync()
    => sql.ExecuteListAsync(
        StoredProcedures.SuperHero.GetAll,
        _ => { },
        MapToSuperHero
    );
    

    public Task<SuperHero> GetByIdAsync(HeroId heroId)
    => sql.ExecuteSingleAsync(
            StoredProcedures.SuperHero.GetById,
            x => x.Add("@HeroId", SqlDbType.UniqueIdentifier).Value = heroId.Value,
            MapToSuperHero
        );

    public async Task UpdateAsync(SuperHero hero)
    {
        await sql.ExecuteAsync(
            StoredProcedures.SuperHero.Update,
            x => x.AddFrom(new SuperHeroUpdateParams()
            {
                HeroId = hero.HeroId.Value,
                HeroName = hero.HeroName.ToString(),
                RealName = hero.RealName.ToString(),
                PowerLevel = hero.PowerLevel.Value,
                Universe = hero.Universe.ToString()
            })
        );
    }
}