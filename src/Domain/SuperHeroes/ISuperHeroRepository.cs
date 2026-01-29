using Domain.Teams;

namespace Domain.SuperHeroes;

public interface ISuperHeroRepository
{
    Task<SuperHero> GetByIdAsync(HeroId heroId);
    Task<IReadOnlyList<SuperHero>> GetAllAsync();

    Task<Guid> AddAsync(SuperHero hero);
    Task UpdateAsync(SuperHero hero);

    Task AssignToTeamAsync(HeroId heroId, TeamId teamId);
}