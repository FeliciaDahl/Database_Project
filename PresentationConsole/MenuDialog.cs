
using Business.Dto;
using Business.Interfaces;
using Business.Models;

namespace PresentationConsole;

public class MenuDialog : IMenuDialog
{
    private readonly IProjectService _projectService;
    private readonly ICostumerService _costumerService;
    private readonly IProjectManagerService _projectManagerService;
    private readonly IServiceService _serviceService;
    private readonly IStatusTypeService _statusTypeService;

    public MenuDialog(IProjectService projectService, ICostumerService costumerService, IProjectManagerService projectManagerService, IServiceService serviceService, IStatusTypeService statusTypeService)
    {
        _projectService = projectService;
        _costumerService = costumerService;
        _projectManagerService = projectManagerService;
        _serviceService = serviceService;
        _statusTypeService = statusTypeService;
    }
    public async Task CreateProjectAsync()
    {
        Console.WriteLine("Ange projektnamn:");
        string title = Console.ReadLine() ?? "";

        Console.WriteLine("Ange beskrivning (valfritt):");
        string? description = Console.ReadLine();

        Console.WriteLine("Ange startdatum (yyyy-MM-dd):");
        DateTime startDate = DateTime.Parse(Console.ReadLine() ?? "");

        Console.WriteLine("Ange slutdatum (yyyy-MM-dd):");
        DateTime endDate = DateTime.Parse(Console.ReadLine() ?? "");

        // Hämta eller skapa Costumer
        Console.WriteLine("Ange kundens namn:");
        string costumerName = Console.ReadLine() ?? "";
        
        var costumerForm = new CostumerRegistrationForm { CostumerName = costumerName };

        var costumer = await _costumerService.GetOrCreateCostumerAsync(costumerForm);

        
        Console.WriteLine("Ange ProjectManager e-post:");
        string email = Console.ReadLine() ?? "";
        Console.WriteLine("Ange förnamn:");
        string firstName = Console.ReadLine() ?? "";
        Console.WriteLine("Ange efternamn:");
        string lastName = Console.ReadLine() ?? "";
        Console.WriteLine("Ange telefon (valfritt):");
        string? phone = Console.ReadLine();

        var PMform = new ProjectManagerRegistrationForm
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Phone = phone
        };


        var projectManager = await _projectManagerService.GetOrCreateProjectManagerAsync(PMform);

        Console.WriteLine("Lägg till service.");
        Console.WriteLine("Service Namn:");
        string service = Console.ReadLine() ?? "";
        Console.WriteLine("Beskrivning:");
        string desc = Console.ReadLine() ?? "";
        Console.WriteLine("Pris:");
        decimal price = decimal.Parse(Console.ReadLine()!);

        var ServiceForm = new ServiceRegistrationForm
        {
            ServiceName = service,
            ServiceDescription = desc,
            Price = price
        };

        var services = await _serviceService.CreateServiceAsync(ServiceForm);
       

        
        Console.WriteLine("Välj projektstatus:");
        var statusTypes = await _statusTypeService.GetAllServicesAsync();
        foreach (var status in statusTypes)
        {
            Console.WriteLine($"{status.Id}: {status.StatusName}");
        }
        int statusTypeId = int.Parse(Console.ReadLine() ?? "");


        Console.WriteLine($"Costumer ID: {costumer?.Id}");
        Console.WriteLine($"ProjectManager ID: {projectManager?.Id}");
        Console.WriteLine($"Service ID: {services?.Id}");
        Console.WriteLine($"StatusType ID: {statusTypeId}");

        if (costumer == null || projectManager == null || services == null)
        {
            Console.WriteLine("Ett eller flera objekt är null. Kontrollera inmatningen.");
            return;
        }



        // Skapa och spara projektet
        var projectRegForm = new ProjectRegistrationForm
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            CostumerId = costumer.Id,
            ProjectManagerId = projectManager.Id,
            ServiceId = services.Id,
            StatusTypeId = statusTypeId,
        
        };

    

    var createdProject = await _projectService.CreateProjectAsync(projectRegForm);

        if (createdProject != null)
        {
            Console.WriteLine("Projekt skapat!");
        }
        else
        {
            Console.WriteLine("Något gick fel vid skapandet av projektet.");
        }
    }


    public async Task ShowProjects()
    {
        var result = await _projectService.GetAllProjectsAsync();

        if (result != null)

            foreach (var project in result)
            {
                Console.WriteLine($"Projekt: {project.Title}, Kund: {project.CostumerName}, " +
                           $"Projektledare: {project.ProjectManagerFirstName} {project.ProjectManagerLastName}, " +
                           $"Service: {project.ServiceName}, Status: {project.ServiceName}");
            }
 

    }
}
