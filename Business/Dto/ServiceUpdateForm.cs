
namespace Business.Dto;

public class ServiceUpdateForm
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;

    public string? ServiceDescription { get; set; }

    public decimal Price { get; set; }

}
