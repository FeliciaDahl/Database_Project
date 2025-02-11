
using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;


namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICostumerService costumerService, IProjectManagerService projectManagerService, IServiceRepository serviceRepository, IStatusTypeService statusTypeService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICostumerService _costumerService = costumerService;
    private readonly IProjectManagerService _projectManagerService = projectManagerService;
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {

        await _projectRepository.BeginTransactionAsync();


        try
        {
            var costumer = await _costumerService.GetCostumerAsync(c => c.Id == form.CostumerId);
            var pm = await _projectManagerService.GetProjectManagerAsync(pm => pm.Id == form.ProjectManagerId);
            var service = await _serviceRepository.GetAsync(s => s.Id == form.ServiceId);
            var status = await _statusTypeService.GetStatusTypeByIdAsync(form.StatusTypeId);

            if (costumer == null || pm == null || service == null || status == null)
            {
                Debug.WriteLine("Required value is missing");
                return null!;
            }
            var projectEntity = ProjectFactory.Create(form);
           
            _projectRepository.Add(projectEntity);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();

           return ProjectFactory.Create(projectEntity);
        }

        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {

        var projectEntity = await _projectRepository.GetAllAsync();

        return projectEntity.Select(entity => ProjectFactory.Create(entity));
    }



    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> predicate, Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null)
    {
        var projectEntity = await _projectRepository.GetAsync(predicate, includeExpression);

        if (projectEntity == null)
        {
            return null!;
        }


        return ProjectFactory.Create(projectEntity);
    }


    public async Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    { 
       
        try
        {
            var existingProjectEntity = await _projectRepository.GetAsync(p => p.Title == form.Title);

            if (existingProjectEntity == null)
            {
                Debug.WriteLine("Project not found");
                return null!;
            }
            await _projectRepository.BeginTransactionAsync();

            var updatedEntity = ProjectFactory.Create(form);

             _projectRepository.Update(updatedEntity);

            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            

            return ProjectFactory.Create(updatedEntity);

        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
           var existingProjectEntity =  await _projectRepository.GetAsync(p =>p.Id == id);

            if (existingProjectEntity == null)
            {
                Debug.WriteLine("Project not found");
                return false;
            }

            await _projectRepository.BeginTransactionAsync();

            _projectRepository.Delete(existingProjectEntity);

            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return false!;
        }

    }
}
