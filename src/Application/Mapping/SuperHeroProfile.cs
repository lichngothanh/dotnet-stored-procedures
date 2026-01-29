using Application.DTOs;
using AutoMapper;
using Domain.SuperHeroes;

namespace Application.Mapping;

public class SuperHeroProfile : Profile
{
    public SuperHeroProfile()
    {
        CreateMap<SuperHero, SuperHeroResponse>() 
            // Using ForCtorParam
            
            // .ForCtorParam("HeroId",
            //     opt => opt.MapFrom(src => src.HeroId.Value.ToString()))
            // .ForCtorParam("HeroName",
            //     opt => opt.MapFrom(src => src.HeroName.ToString()))
            // .ForCtorParam("RealName",
            //     opt => opt.MapFrom(src => src.RealName.ToString()))
            // .ForCtorParam("PowerLevel",
            //     opt => opt.MapFrom(src => src.PowerLevel.Value))
            // .ForCtorParam("Universe",
            //     opt => opt.MapFrom(src => src.Universe.ToString()))
            // .ForCtorParam("TeamId",
            //     opt => opt.MapFrom(src => src.TeamId != null ? src.TeamId.ToString() : null));
            
            // Or using ConvertUsing
            .ConvertUsing(src => new SuperHeroResponse(
                src.HeroId.Value.ToString(),
                src.HeroName.ToString(),
                src.RealName.ToString(),
                src.PowerLevel.Value,
                src.Universe.ToString(),
                src.TeamId != null ? src.TeamId.ToString() : null
            ));
    }
}