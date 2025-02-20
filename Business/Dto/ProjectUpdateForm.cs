
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ProjectUpdateForm
{

    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required]
    public int CostumerId { get; set; }

    [Required]
    public int ProjectManagerId { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int StatusTypeId { get; set; }

}
