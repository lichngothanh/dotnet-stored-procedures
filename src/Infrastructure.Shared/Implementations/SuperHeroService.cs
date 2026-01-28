using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Common;
using Domain.SuperHeroes;
using Domain.Teams;

namespace Infrastructure.Shared.Implementations;

public class SuperHeroService(ISuperHeroRepository superHeroRepository, IMapper mapper) : ISuperHeroService
{
    public async Task<SuperHeroResponse> GetByIdAsync(Guid heroId)
    {
        var heroResponse = await superHeroRepository.GetByIdAsync(HeroId.From(heroId));
        
        return mapper.Map<SuperHeroResponse>(heroResponse);
    }

    public async Task<IReadOnlyList<SuperHeroResponse>> GetAllAsync()
    {
        var heroes = await superHeroRepository.GetAllAsync();
        
        return mapper.Map<IReadOnlyList<SuperHeroResponse>>(heroes);
    }

    public async Task<Guid> CreateAsync(CreateSuperHeroRequest request)
    {
        var hero = new SuperHero(
            HeroName.From(request.HeroName),
            RealName.From(request.RealName),
            PowerLevel.From(request.PowerLevel),
            Universe.From(request.Universe)
        );
        
        var heroId = await superHeroRepository.AddAsync(hero);
        return heroId;
    }

    public async Task UpdateAsync(UpdateSuperHeroRequest request)
    {
        var hero = await superHeroRepository.GetByIdAsync(HeroId.From(request.HeroId));
        hero.Update(
            HeroName.From(request.HeroName),
            RealName.From(request.RealName),
            PowerLevel.From(request.PowerLevel),
            Universe.From(request.Universe)
        );
        
        await superHeroRepository.UpdateAsync(hero);
    }
}