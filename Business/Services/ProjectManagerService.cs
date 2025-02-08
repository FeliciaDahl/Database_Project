
using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectManagerService : IProjectManagerService
{
    private readonly IProjectManagerRepository _projectManagerRepository;

    public ProjectManagerService(IProjectManagerRepository projectManagerRepository)
    {
        _projectManagerRepository = projectManagerRepository;
    }
    /// Nästa steg är att koll om det är bättre att skriva som Costumer . är det bättre kod. Sedan skapa Interfaces . Nästa steg kolla om koden fungerar.
   //Kolla igenom all kod i services så det ser rätt ut, inget blandat enhetligt osv ! BRA JOBBAT IDAG ! IMORGON KÖR VI IGEN! <3
    public async Task<ProjectManagerEntity> GetOrCreateProjectManagerAsync(ProjectManagerRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Email))
            return null!;

        var projectManager = await _projectManagerRepository.GetAsync(pm => pm.Email.ToLower() == form.Email.Trim().ToLower());

        if (projectManager != null)
            return projectManager;

        try
        {
            projectManager = ProjectManagerFactory.Create(form);
            return await _projectManagerRepository.CreateAsync(projectManager);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<ProjectManager>> GetAllProjectManagersAsync()
    {
        var pmEntity = await _projectManagerRepository.GetAllAsync();

        return pmEntity.Select(entity => ProjectManagerFactory.Create(entity));
    }


    public async Task<ProjectManager?> GetProjectManagerAsync(Expression<Func<ProjectManagerEntity, bool>> expression, Func<IQueryable<ProjectManagerEntity>, IQueryable<ProjectManagerEntity>>? includeExpression = null)
    {
        var pmEntity = await _projectManagerRepository.GetAsync(expression, includeExpression);

        if (pmEntity == null)
        {
            return null;
        }

        return ProjectManagerFactory.Create(pmEntity);
    }

    public async Task<ProjectManager> UpdateProjectManagerAsync(ProjectManagerUpdateForm form)
    {
        try
        {
            var existingEntity = await _projectManagerRepository.GetAsync(pm => pm.Email == form.Email);

            if (existingEntity == null)
            {
                return null!;
            }


            var updatedEntity = ProjectManagerFactory.Create(form);

            var result = await _projectManagerRepository.UpdateAsync(pm => pm.Email == form.Email, updatedEntity);
            if (result == null)
            {
                return null!;
            }


            return ProjectManagerFactory.Create(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ett fel uppstod vid uppdatering: {ex.Message}");
            return null!;
        }

    }
    public async Task<bool> DeleteProjectManagerAsync(int id)
    {
        try
        {
            var result = await _projectManagerRepository.DeleteAsync(p => p.Id == id);

            if (!result)
            {
                Debug.WriteLine($"Could not delete projectmanager {id}.");
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
