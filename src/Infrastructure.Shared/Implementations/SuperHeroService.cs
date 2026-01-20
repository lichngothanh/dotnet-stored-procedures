using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.SuperHeroes;

namespace Infrastructure.Shared.Implementations;

public class SuperHeroService(ISuperHeroRepository superHeroRepository, IMapper mapper) : ISuperHeroService
{
    private readonly ISuperHeroRepository _superHeroRepository = superHeroRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<SuperHeroResponse> GetByIdAsync(Guid heroId)
    {
        var heroResponse = await _superHeroRepository.GetByIdAsync(HeroId.From(heroId));
        
        return _mapper.Map<SuperHeroResponse>(heroResponse);
    }
}