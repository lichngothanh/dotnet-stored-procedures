using Domain.Common;
using Domain.Teams;

namespace Domain.SuperHeroes;

public class SuperHero
{
    public HeroId HeroId { get; private set; } = null!;
    public HeroName HeroName { get; private set; } = null!;
    public RealName RealName { get; private set; } = null!;
    public PowerLevel PowerLevel { get; private set; } = null!;
    public Universe Universe { get; private set; } = null!;
    
    public TeamId? TeamId { get; private set; }

    private SuperHero()
    {
    }

    public SuperHero(
        HeroId heroId,
        HeroName heroName,
        RealName realName,
        PowerLevel powerLevel,
        Universe universe)
    {
        HeroId = heroId;
        HeroName = heroName;
        RealName = realName;
        PowerLevel = powerLevel;
        Universe = universe;
    }
    
    public void AssignToTeam(TeamId teamCode)
    {
        TeamId = teamCode;
    }
}