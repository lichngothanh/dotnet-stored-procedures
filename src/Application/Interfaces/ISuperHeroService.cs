using Application.DTOs;

namespace Application.Interfaces;

public interface ISuperHeroService
{
    Task<SuperHeroResponse> GetByIdAsync(Guid heroId);
}