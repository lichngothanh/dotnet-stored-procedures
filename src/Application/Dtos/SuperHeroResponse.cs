namespace Application.DTOs;

public record SuperHeroResponse
{
    public string HeroId { get; init; } = string.Empty;
    public string HeroName { get; init; } = string.Empty;
    public string RealName { get; init; } = string.Empty;
    public int PowerLevel { get; init; }
    public string Universe { get; init; } = string.Empty;
    public string? TeamId { get; init; }
}