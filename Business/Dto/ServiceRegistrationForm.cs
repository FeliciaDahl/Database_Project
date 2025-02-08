
namespace Business.Dto;

public class ServiceRegistrationForm
{
    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }

    public decimal Price { get; set; }
}
