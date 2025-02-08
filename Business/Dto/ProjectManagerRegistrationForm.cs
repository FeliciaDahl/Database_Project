using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ProjectManagerRegistrationForm
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]  
    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

}
