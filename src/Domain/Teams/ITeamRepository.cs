using Domain.Common;

namespace Domain.Teams;

public interface ITeamRepository
{
    Task<Team> GetByIdAsync(TeamId teamId);
    Task<IReadOnlyList<Team>> GetAllAsync();
    Task<IReadOnlyList<Team>> GetByUniverseAsync(Universe universe);
}