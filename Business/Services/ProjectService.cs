
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

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
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
                return false;
            }
            var projectEntity = ProjectFactory.Create(form);

            _projectRepository.Add(projectEntity);
            await _projectRepository.SaveAsync();

            await _projectRepository.CommitTransactionAsync();
                return true; 
        }

        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return false;
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


    public async Task<Project> UpdateProjectAsync(int id, ProjectUpdateForm form)
    { 
       
        try
        {
            var existingEntity = await _projectRepository.GetAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                Debug.WriteLine("Project not found");
                return null!;
            }
            await _projectRepository.BeginTransactionAsync();

            existingEntity.Title = string.IsNullOrWhiteSpace(form.Title) ? existingEntity.Title : form.Title;
            existingEntity.Description = string.IsNullOrWhiteSpace(form.Description) ? existingEntity.Description : form.Description;
            existingEntity.CostumerId = form.CostumerId > 0 ? form.CostumerId : existingEntity.CostumerId;
            existingEntity.ProjectManagerId = form.ProjectManagerId > 0 ? form.ProjectManagerId : existingEntity.ProjectManagerId;
            existingEntity.ServiceId = form.ServiceId > 0 ? form.ServiceId : existingEntity.ServiceId;
            existingEntity.StatusTypeId = form.StatusTypeId > 0 ? form.StatusTypeId : existingEntity.StatusTypeId;

            _projectRepository.Update(existingEntity);

            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();

            var updatedProject = ProjectFactory.Create(existingEntity);
       
            return updatedProject;

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

    //public async Task<IEnumerable<Project>> SearchProjectsAsync(string category, string query)
    //{
    //    var projectEntites = await _projectRepository.GetAllAsync();

    //    var projects = projectEntites.Select(p => ProjectFactory.Create(projectEntites)).ToList();

    //    return category.ToLower() switch
    //    {
    //        "id" => projects.Where(p => p.Id.ToString().Contains(query)).ToList(),
    //        "title" => projects.Where(p => p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList(),
    //        "projectmanager" => projects.Where(p => p.ProjectManager.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList(),
    //        "costumer" => projects.Where(p => p.Costumer.CostumerName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList(),
    //        _ => new List<Project>() // Om en okänd kategori används, returnera tom lista
    //    };
    //}


}
