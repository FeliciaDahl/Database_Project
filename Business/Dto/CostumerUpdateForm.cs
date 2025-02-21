
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class CostumerUpdateForm
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string CostumerName { get; set; } = null!;
}
