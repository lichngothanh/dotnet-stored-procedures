using Domain.Common;
using Domain.Teams;

namespace Infrastructure.Ado.Persistence;

public class TeamRepository : ITeamRepository
{
    public Task<Team> GetByIdAsync(TeamId teamId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Team>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Team>> GetByUniverseAsync(Universe universe)
    {
        throw new NotImplementedException();
    }
}