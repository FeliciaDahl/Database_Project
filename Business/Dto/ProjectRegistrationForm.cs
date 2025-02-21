

using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ProjectRegistrationForm
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    [Required]
    public int CostumerId{ get; set; }
    [Required]
    public int ProjectManagerId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int StatusTypeId { get; set; }

}
