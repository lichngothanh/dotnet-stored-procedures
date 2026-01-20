using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroesController(ISuperHeroService superHeroService) : ControllerBase
{
    private readonly ISuperHeroService _superHeroService = superHeroService;

    [HttpGet("{heroId:guid}")]
    public async Task<IActionResult> GetById(Guid heroId)
    {
        var response = await _superHeroService.GetByIdAsync(heroId);
        return Ok(response);
    }
}
