using Domain.Common;

namespace Domain.Teams;

public class Team
{
    public TeamId TeamId { get; private set; } = null!;
    public TeamName TeamName { get; private set; } = null!;
    public Universe Universe { get; private set; } = null!;

    public Team()
    {
        
    }

    public Team(TeamId teamId, TeamName teamName, Universe universe)
    {
        TeamId = teamId;
        TeamName = teamName;
        Universe = universe;
    }
}