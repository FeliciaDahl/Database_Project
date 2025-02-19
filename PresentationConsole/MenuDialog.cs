
using Business.Dto;
using Business.Interfaces;
using Business.Models;
using System.ComponentModel.Design;

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

    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("---Menu---");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. Show Projects");
            Console.WriteLine("3. Show Costumers");
            Console.WriteLine("4. Show ProjectManagers");
            Console.WriteLine("5. Update Project");
            Console.WriteLine("6. Delete Project");
            Console.WriteLine("7. Quit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProjectAsync();
                    break;
                case "2":
                    await ShowProjects();
                    break;
                case "3":
                    await ShowCostumers();
                    break;
                case "4":
                    await ShowProjectManagers();
                    break;
                case "5":
                    await UpdateProject();
                    break;
                case "6":
                    await DeleteProject();
                    break;
                //case "5":
                //    await
                //    break;
                //default:
                //    break;

            }
        }
    }

    public async Task CreateProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("---Project Information---");
        Console.WriteLine("Project Title:");
        string title = Console.ReadLine() ?? "";

        Console.WriteLine("Description:");
        string? description = Console.ReadLine();

        Console.WriteLine("StartDate (yyyy-MM-dd):");
        DateTime startDate = DateTime.Parse(Console.ReadLine() ?? "");

        Console.WriteLine("EndDate (yyyy-MM-dd):");
        DateTime endDate = DateTime.Parse(Console.ReadLine() ?? "");

        Console.Clear();
        Console.WriteLine("---Costumer Information---");
        Console.WriteLine("Costumer Name:");
        string costumerName = Console.ReadLine() ?? "";
        
        var costumerForm = new CostumerRegistrationForm { CostumerName = costumerName };

        var costumer = await _costumerService.CreateCostumerAsync(costumerForm);
        Console.Clear();
        Console.WriteLine("---ProjectManager Information---");
        Console.WriteLine("First Name:");
        string firstName = Console.ReadLine() ?? "";
        Console.WriteLine("Last Name:");
        string lastName = Console.ReadLine() ?? "";
        Console.WriteLine("Email:");
        string email = Console.ReadLine() ?? "";
       
        Console.WriteLine("Phone:");
        string? phone = Console.ReadLine();

        var PMform = new ProjectManagerRegistrationForm
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Phone = phone
        };

        var projectManager = await _projectManagerService.CreateProjectManagerAsync(PMform);

        Console.Clear();
        Console.WriteLine("---Service Information---");
        Console.WriteLine("Service Name:");
        string service = Console.ReadLine() ?? "";
        Console.WriteLine("Service Description:");
        string desc = Console.ReadLine() ?? "";
        Console.WriteLine("Price:");
        decimal price = decimal.Parse(Console.ReadLine()!);

        var serviceForm = new ServiceRegistrationForm
        {
            ServiceName = service,
            ServiceDescription = desc,
            Price = price
        };

        var services = await _serviceService.CreateServiceAsync(serviceForm);


        Console.Clear();
        Console.WriteLine("---Project Status---");
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
            Console.WriteLine("Required fields are missing, try again.");
            return;
        }

        var projectForm = new ProjectRegistrationForm
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

    var result = await _projectService.CreateProjectAsync(projectForm);

        if (result)
        {
            Console.WriteLine("Project Created!");
        }
        else
        {
            Console.WriteLine("Something went wrong, try again");
        }
    }


    public async Task ShowProjects()
    {
        Console.Clear();
        Console.WriteLine("---Projects---");
        var result = await _projectService.GetAllProjectsAsync();

        if (result != null)

            foreach (var project in result)
            {
                Console.WriteLine($"Project ID: {project.Id}\n" +
                  $"Project Title: {project.Title}\n" +
                  $"Costumer: {project.CostumerName}\n" +
                  $"Project Manager: {project.ProjectManagerFirstName} {project.ProjectManagerLastName}\n" +
                  $"Service: {project.ServiceName}\n" +
                  $"Project Status: {project.StatusTypeName}");
                Console.WriteLine();
            }
        Console.ReadKey();
    }

    public async Task ShowCostumers()
    {
        Console.Clear();
        Console.WriteLine("---Costumers---");
        var result = await _costumerService.GetAllCostumersAsync();

        if(result != null)

            foreach(var costumer in result)
            {
                Console.WriteLine($"CostumerName : {costumer.CostumerName}");
            }
        Console.ReadKey();
    }

    public async Task ShowProjectManagers()
    {
        Console.Clear();
        Console.WriteLine("---Project Managers---");
        var result = await _projectManagerService.GetAllProjectManagersAsync();

        if (result != null)
            foreach (var PM in result)
            {
                Console.WriteLine($"ProjectManager Name : {PM.FirstName} {PM.LastName}\n" +
                    $"Email: {PM.Email}\n" + 
                    $"Phone: {PM.Phone}\n");
                Console.WriteLine();
            }
        Console.ReadKey();
    }


    public async Task UpdateProject()
    {
        Console.Clear();
        Console.WriteLine("---Update project---");

        var existingProject = await ShowAndSelectProject();

        if(existingProject == null)
        {
            Console.WriteLine("No project found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Updating Project {existingProject.Id} {existingProject.Title}");

        Console.Write($"New Title (current: {existingProject.Title}, leave empty to keep): ");
        string title = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(title)) _ = existingProject.Title;

        Console.Write($"New Description (current: {existingProject.Description}, leave empty to keep): ");
        string? description = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(description)) _ = existingProject.Description;

        Console.WriteLine($"New Start Date (current: {existingProject.StartDate}, Leave empty to keep current):");
        string startDateInput = Console.ReadLine()!;
        DateTime startDate = string.IsNullOrWhiteSpace(startDateInput) ? existingProject.StartDate : DateTime.Parse(startDateInput);

        Console.WriteLine($"New End Date (current: {existingProject.EndDate} Leave empty to keep current):");
        string endDateInput = Console.ReadLine()!;
        DateTime endDate = string.IsNullOrWhiteSpace(endDateInput) ? existingProject.EndDate : DateTime.Parse(endDateInput);

        Console.Clear();
        Console.WriteLine("--- Update Costumer ---");
        Console.WriteLine("Do you want to edit Costumer information? (y/n)");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write($"New Costumer Name (current: {existingProject.CostumerName}, leave empty to keep): ");
            string costumerName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(costumerName)) _ = existingProject.CostumerName;


            var costumerForm = new CostumerUpdateForm { CostumerName = costumerName };
            var costumer = await _costumerService.UpdateCostumerAsync(existingProject.CostumerId, costumerForm);

            existingProject.CostumerId = costumer?.Id ?? existingProject.CostumerId;
        }

        Console.Clear();
        Console.WriteLine("--- Update Project Manager ---");
        Console.WriteLine("Do you want to edit Project Manager information? (y/n)");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write($"New Project Manager First Name (current: {existingProject.ProjectManagerFirstName}, leave empty to keep): ");
            string firstName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(firstName)) firstName = existingProject.ProjectManagerFirstName;

            Console.Write($"New Project Manager Last Name (current: {existingProject.ProjectManagerLastName}, leave empty to keep): ");
            string lastName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(lastName)) lastName = existingProject.ProjectManagerLastName;

            Console.Write($"New Project Manager Email (current: {existingProject.ProjectManagerEmail}, leave empty to keep): ");
            string email = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(email)) email = existingProject.ProjectManagerEmail;

            Console.Write($"New Project Manager Phone (current: {existingProject.ProjectManagerPhone}, leave empty to keep): ");
            string? phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone)) phone = existingProject.ProjectManagerPhone;

            var pmForm = new ProjectManagerUpdateForm
            {
                Id = existingProject.ProjectManagerId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone
            };

            var projectManager = await _projectManagerService.UpdateProjectManagerAsync(existingProject.ProjectManagerId, pmForm);

            existingProject.ProjectManagerId = projectManager?.Id ?? existingProject.ProjectManagerId;
        }

        Console.Clear();
        Console.WriteLine("--- Update Service ---");
        Console.WriteLine("Do you want to edit Service information? (y/n)");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write($"New Service Name (current: {existingProject.ServiceName}, leave empty to keep): ");
            string serviceName = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(serviceName)) serviceName = existingProject.ServiceName;

            Console.Write($"New Service Description (current: {existingProject.ServiceDescription}, leave empty to keep): ");
            string serviceDescription = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(serviceDescription)) serviceDescription = existingProject.ServiceDescription;

            Console.Write($"New Service Price (current: {existingProject.ServicePrice}, leave empty to keep): ");
            string servicePriceInput = Console.ReadLine()!;
            decimal price = existingProject.ServicePrice;
            if (!string.IsNullOrWhiteSpace(servicePriceInput) && decimal.TryParse(servicePriceInput, out decimal newPrice))
            {
                price = newPrice;
            }

            var serviceForm = new ServiceUpdateForm
            {
                Id = existingProject.ServiceId,
                ServiceName = serviceName,
                ServiceDescription = serviceDescription,
                Price = price
            };

            var service = await _serviceService.UpdateServiceAsync(existingProject.ServiceId, serviceForm);

            existingProject.ServiceId = service?.Id ?? existingProject.ServiceId;
        }

        Console.Clear();
        Console.WriteLine("--- Update Project Status ---");
        var statusTypes = await _statusTypeService.GetAllServicesAsync();
        foreach (var status in statusTypes)
        {
            Console.WriteLine($"{status.Id}: {status.StatusName}");
        }

        Console.WriteLine("Enter new StatusType ID (Leave empty to keep current):");
        string statusInput = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(statusInput) && int.TryParse(statusInput, out int statusTypeId))
        {
            existingProject.StatusTypeId = statusTypeId;
        }

        var updateForm = new ProjectUpdateForm
        {
            Id = existingProject.Id,
            Title = existingProject.Title,
            Description = existingProject.Description,
            StartDate = existingProject.StartDate,
            EndDate = existingProject.EndDate,
            CostumerId = existingProject.CostumerId,
            ProjectManagerId = existingProject.ProjectManagerId,
            ServiceId = existingProject.ServiceId,
            StatusTypeId = existingProject.StatusTypeId
        };

        var result = await _projectService.UpdateProjectAsync(existingProject.Id, updateForm);

        if (result)
        {
            Console.WriteLine("Project updated successfully!");
        }
        else
        {
            Console.WriteLine("Something went wrong, try again.");
        }
    }

    public async Task<bool> DeleteProject()
    {
        Console.Clear();
        Console.WriteLine("---Delete project---");

        var existingProject = await ShowAndSelectProject();

        if (existingProject == null)
        {
            Console.WriteLine("No project found");
            Console.ReadKey();
            return false;
        }

        Console.WriteLine($"Are you sure you want to delete Project: {existingProject.Id} {existingProject.Title}? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _projectService.DeleteProjectAsync(existingProject.Id);

            if (result)
            {
                Console.WriteLine("Project was deleted successfully");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Project was not deleted");
                Console.ReadKey();
                return false;
            }

        }
        else
        {
            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
            return false;
        }
        
  
    }


    

    public async Task<Project?> ShowAndSelectProject()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        if(projects == null || !projects.Any())
        {
            Console.WriteLine("No projects found");
            Console.ReadKey();
            return null!;
        }

        Console.WriteLine("---Project List---");
        foreach (var project in projects)
        {
            Console.WriteLine($"Project ID: {project.Id}, Project Title: {project.Title}, Costumer: {project.CostumerName}");
        }
        Console.WriteLine("Enter the ID of project you want to handle");
        if (!int.TryParse(Console.ReadLine(), out var projectId))
        {
            Console.WriteLine("Invalid Id, try again");
            Console.ReadKey();
            return null;
        }

        var existingProject = projects.FirstOrDefault(x => x.Id == projectId);
        if (existingProject == null)
        {
            Console.WriteLine("Project not found");
            Console.ReadKey();
            return null;
        }

        return existingProject;
    }
}
