using Application.DTOs;
using AutoMapper;
using Domain.SuperHeroes;

namespace Application.Mapping;

public class SuperHeroProfile : Profile
{
    public SuperHeroProfile()
    {
        CreateMap<SuperHero, SuperHeroResponse>()
            .ForMember(dest => dest.HeroId, 
                opt => opt.MapFrom(src => src.HeroId.Value.ToString()))
            .ForMember(dest => dest.HeroName, 
                opt => opt.MapFrom(src => src.HeroName.ToString()))
            .ForMember(dest => dest.RealName, 
                opt => opt.MapFrom(src => src.RealName.ToString()))
            .ForMember(dest => dest.PowerLevel, 
                opt => opt.MapFrom(src => src.PowerLevel.ToString()))
            .ForMember(dest => dest.Universe, 
                opt => opt.MapFrom(src => src.Universe.ToString()))
            .ForMember(dest => dest.TeamId, 
                opt => opt.MapFrom(src => src.TeamId != null ? src.TeamId.ToString() : null));
    }
}