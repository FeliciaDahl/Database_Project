

namespace Business.Dto;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int CostumerId{ get; set; }

    public int ProjectManagerId { get; set; } 

    public int ServiceId { get; set; } 

    public int StatusTypeId { get; set; }

}
