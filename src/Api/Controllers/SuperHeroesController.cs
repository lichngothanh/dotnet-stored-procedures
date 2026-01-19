using Domain.SuperHeroes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroesController(ISuperHeroRepository repository) : ControllerBase
{
    private readonly ISuperHeroRepository _repository = repository;

    [HttpGet("{heroId:guid}")]
    public async Task<IActionResult> GetById(Guid heroId)
    {
        var hero = await _repository.GetByIdAsync(HeroId.From(heroId));

        var response = new SuperHeroResponse
        {
            HeroId = hero.HeroId.ToString(),
            HeroName = hero.HeroName.ToString(),
            RealName = hero.RealName.ToString(),
            PowerLevel = int.Parse(hero.PowerLevel.ToString()),
            Universe = hero.Universe.ToString(),
            TeamId = hero.TeamId?.ToString()
        };

        return Ok(response);
    }
}

public record SuperHeroResponse
{
    public string HeroId { get; init; } = string.Empty;
    public string HeroName { get; init; } = string.Empty;
    public string RealName { get; init; } = string.Empty;
    public int PowerLevel { get; init; }
    public string Universe { get; init; } = string.Empty;
    public string? TeamId { get; init; }
}
