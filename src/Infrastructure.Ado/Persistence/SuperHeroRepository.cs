using Domain.SuperHeroes;
using Domain.Teams;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Constants;
using System.Data;
using static Infrastructure.Ado.Mapping.SuperHeroMapper;
namespace Infrastructure.Ado.Persistence;

public class SuperHeroRepository(ISqlExecutor sql) : ISuperHeroRepository
{

    private readonly ISqlExecutor _sql = sql;
    
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