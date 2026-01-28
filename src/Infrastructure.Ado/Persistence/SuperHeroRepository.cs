using Domain.SuperHeroes;
using Domain.Teams;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Constants;
using System.Data;
using static Infrastructure.Ado.Mapping.SuperHeroMapper;
namespace Infrastructure.Ado.Persistence;

public class SuperHeroRepository(ISqlExecutor sql) : ISuperHeroRepository
{
    public async Task<Guid> AddAsync(SuperHero hero)
    {
        var heroId = await sql.ExecuteSingleValueAsync<Guid>(
            StoredProcedures.SuperHero.Insert,
            x =>
            {
                x.Add("@HeroName", SqlDbType.NVarChar, 100).Value = hero.HeroName.ToString();
                x.Add("@RealName", SqlDbType.NVarChar, 150).Value = hero.RealName.ToString();
                x.Add("@PowerLevel", SqlDbType.Int).Value = hero.PowerLevel.Value;
                x.Add("@Universe", SqlDbType.NVarChar, 20).Value = hero.Universe.ToString();
                x.Add("@TeamId", SqlDbType.UniqueIdentifier).Value =
                    hero.TeamId ?? (object)DBNull.Value;
            }
        );

        return heroId;
    }

    public Task AssignToTeamAsync(HeroId heroId, TeamId teamId)
    {
        throw new NotImplementedException();
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

    public Task UpdateAsync(SuperHero hero)
    => sql.ExecuteAsync(
        StoredProcedures.SuperHero.Update,
        x =>
        {
            x.Add("@HeroId", SqlDbType.UniqueIdentifier).Value = hero.HeroId.Value;
            x.Add("@HeroName", SqlDbType.NVarChar, 100).Value = hero.HeroName.ToString();
            x.Add("@RealName", SqlDbType.NVarChar, 150).Value = hero.RealName.ToString();
            x.Add("@PowerLevel", SqlDbType.Int).Value = hero.PowerLevel.Value;
            x.Add("@Universe", SqlDbType.NVarChar, 20).Value = hero.Universe.ToString();
        }
    );
}