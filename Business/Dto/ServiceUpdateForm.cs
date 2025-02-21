
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class ServiceUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }
    [Required]
    public decimal Price { get; set; }

}
