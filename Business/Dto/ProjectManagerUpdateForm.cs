
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ProjectManagerUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    public string? Phone { get; set; }
}
