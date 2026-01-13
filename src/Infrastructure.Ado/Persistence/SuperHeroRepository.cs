using Domain.SuperHeroes;
using Domain.Teams;

namespace Infrastructure.Ado.Persistence;

public class SuperHeroRepository : ISuperHeroRepository
{
    public Task<SuperHero?> GetByIdAsync(HeroId heroId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SuperHero>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(SuperHero hero)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SuperHero hero)
    {
        throw new NotImplementedException();
    }

    public Task AssignToTeamAsync(HeroId heroId, TeamId teamId)
    {
        throw new NotImplementedException();
    }
}