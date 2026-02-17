using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class AssignToTeamRequest
{
    [Required(ErrorMessage = "Hero ID is required")]
    public Guid HeroId { get; init; }
    
    [Required(ErrorMessage = "Team ID is required")]
    public Guid TeamId { get; init; }
}