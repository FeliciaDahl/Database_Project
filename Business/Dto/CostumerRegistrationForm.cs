
using System.ComponentModel.DataAnnotations;

namespace Business.Dto;

public class CostumerRegistrationForm
{
    [Required]
    public string CostumerName { get; set; } = null!;
}
