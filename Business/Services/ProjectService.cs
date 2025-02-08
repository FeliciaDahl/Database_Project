
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

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICostumerRepository _costumerRepository;
    private readonly IProjectManagerRepository _projectManagerRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IStatusTypeRepository _statusTypeRepository;

    public ProjectService(IProjectRepository projectRepository, ICostumerRepository costumerRepository, IProjectManagerRepository projectManagerRepository, IServiceRepository serviceRepository, IStatusTypeRepository statusTypeRepository)
    {
        _projectRepository = projectRepository;
        _costumerRepository = costumerRepository;
        _projectManagerRepository = projectManagerRepository;
        _serviceRepository = serviceRepository;
        _statusTypeRepository = statusTypeRepository;
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectRegistrationForm form)
    {
        try
        {
            var costumer = await _costumerRepository.GetAsync(c => c.Id == form.CostumerId);
            var pm = await _projectManagerRepository.GetAsync(pm => pm.Id == form.ProjectManagerId);
            var service = await _serviceRepository.GetAsync(s => s.Id == form.ServiceId);
            var status = await _statusTypeRepository.GetAsync(s => s.Id == form.StatusTypeId);

            if (costumer == null || pm == null || service == null || status == null)
            {
                Debug.WriteLine("Required value missing");
                return null!;
            }

            var projectEntity = ProjectFactory.Create(form);

            var createdProject = await _projectRepository.CreateAsync(projectEntity);
            return createdProject;

        }

        catch (Exception ex)
        {
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
                return null!;
            }

            var updatedEntity = ProjectFactory.Create(form);

            var result = await _projectRepository.UpdateAsync(p => p.Title == form.Title, updatedEntity);
            if (result == null)
            {
                return null!;
            }

            return ProjectFactory.Create(result);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        try
        {
            var result = await _projectRepository.DeleteAsync(p => p.Id == id);
            if (!result)
            {
                Debug.WriteLine($"Could not delete project {id}.");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return false!;
        }

    }
}
