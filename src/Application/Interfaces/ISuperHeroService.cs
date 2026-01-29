﻿using Application.DTOs;

namespace Application.Interfaces;

public interface ISuperHeroService
{
    Task<SuperHeroResponse> GetByIdAsync(Guid heroId);
    Task<IReadOnlyList<SuperHeroResponse>> GetAllAsync();
    Task<Guid> CreateAsync(CreateSuperHeroRequest request);
    Task UpdateAsync(UpdateSuperHeroRequest request);
    Task AssignToTeamAsync(AssignToTeamRequest request);
}