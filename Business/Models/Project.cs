
namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int CostumerId { get; set; }

    public int ProjectManagerId { get; set; }

    public int ServiceId { get; set; }

    public int StatusTypeId { get; set; }

    public string CostumerName { get; set; } = null!;
    public string ProjectManagerFirstName { get; set; } = null!;
    public string ProjectManagerLastName { get; set; } = null!;
    public string ProjectManagerEmail { get; set; } = null!;
    public string? ProjectManagerPhone { get; set; }
    public string ServiceName { get; set; } = null!;
    public string ServiceDescription { get; set; } = null!;
    public decimal ServicePrice { get; set; }
    public string StatusTypeName { get; set; } = null!;


}
