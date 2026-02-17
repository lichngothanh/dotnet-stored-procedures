using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuperHeroesController(ISuperHeroService superHeroService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await superHeroService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{heroId:guid}")]
    public async Task<IActionResult> GetById(Guid heroId)
    {
        var response = await superHeroService.GetByIdAsync(heroId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSuperHeroRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var heroId = await superHeroService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetById),
            new { heroId },
            new { message = "SuperHero created successfully", heroId }
        );
    }

    [HttpPut("{heroId:guid}")]
    public async Task<IActionResult> Update(Guid heroId, [FromBody] UpdateSuperHeroRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (heroId != request.HeroId)
        {
            return BadRequest(new { message = "HeroId mismatch" });
        }

        await superHeroService.UpdateAsync(request);
        return Ok(new { message = "SuperHero updated successfully" });
    }

    [HttpPost("assign-to-team")]
    public async Task<IActionResult> AssignToTeam([FromBody] AssignToTeamRequest request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await superHeroService.AssignToTeamAsync(request);
        return Ok(new { message = "SuperHero assigned to team successfully" });
    }
}
