using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record UpdateSuperHeroRequest
{
    [Required(ErrorMessage = "Hero ID is required")]
    public Guid HeroId { get; init; }
    
    [Required(ErrorMessage = "Hero name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Hero name must be between 2 and 100 characters")]
    public string HeroName { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Real name is required")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Real name must be between 2 and 150 characters")]
    public string RealName { get; init; } = string.Empty;
    
    [Range(1, 100, ErrorMessage = "Power level must be between 0 and 100")]
    public int PowerLevel { get; init; }
    
    [Required(ErrorMessage = "Universe is required")]
    [RegularExpression("^(Marvel|DC)$", ErrorMessage = "Universe must be either 'Marvel' or 'DC'")]
    public string Universe { get; init; } = string.Empty;
}
