namespace Application.DTOs;

public record SuperHeroResponse(
    string HeroId,
    string HeroName,
    string RealName,
    int PowerLevel,
    string Universe,
    string? TeamId);