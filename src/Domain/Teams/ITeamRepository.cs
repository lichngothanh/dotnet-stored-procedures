namespace Domain.Teams;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(TeamId teamId);
    Task<IReadOnlyList<Team>> GetAllAsync();
}