
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ServiceRegistrationForm
{
    [Required]
    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }
    [Required]
    public decimal Price { get; set; }
}
