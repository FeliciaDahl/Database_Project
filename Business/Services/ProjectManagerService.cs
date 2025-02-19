
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
   
    public async Task<ProjectManager> CreateProjectManagerAsync(ProjectManagerRegistrationForm form)
    {
      
        if (string.IsNullOrWhiteSpace(form.Email))
            return null!;

        await _projectManagerRepository.BeginTransactionAsync();

        var projectManagerEntity = await _projectManagerRepository.GetAsync(pm => pm.Email.ToLower() == form.Email.Trim().ToLower());

        if (projectManagerEntity == null)
        { 
            try
            {
                projectManagerEntity = ProjectManagerFactory.Create(form);
                _projectManagerRepository.Add(projectManagerEntity);
                await _projectManagerRepository.SaveAsync();

                await _projectManagerRepository.CommitTransactionAsync();

               
            }
            catch (Exception ex)
            {
                await _projectManagerRepository.RollbackTransactionAsync();

                Debug.WriteLine($"Error: {ex.Message}");
                return null!;
            }
        }
            return ProjectManagerFactory.Create(projectManagerEntity);
    }

    public async Task<IEnumerable<ProjectManager>> GetAllProjectManagersAsync()
    {
        var projectManagerEntity = await _projectManagerRepository.GetAllAsync();

        return projectManagerEntity.Select(entity => ProjectManagerFactory.Create(entity));
    }


    public async Task<ProjectManager?> GetProjectManagerAsync(Expression<Func<ProjectManagerEntity, bool>> expression, Func<IQueryable<ProjectManagerEntity>, IQueryable<ProjectManagerEntity>>? includeExpression = null)
    {
        var projectManagerEntity = await _projectManagerRepository.GetAsync(expression, includeExpression);

        if (projectManagerEntity == null)
        {
            return null;
        }

        return ProjectManagerFactory.Create(projectManagerEntity);
    }

    public async Task<ProjectManager> UpdateProjectManagerAsync(int id, ProjectManagerUpdateForm form)
    {
        try
        {
            var existingEntity = await _projectManagerRepository.GetAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                Debug.WriteLine("Project manager not found");
                return null!;
            }

            await _projectManagerRepository.BeginTransactionAsync();

            existingEntity.FirstName = string.IsNullOrWhiteSpace(form.FirstName) ? existingEntity.FirstName : form.FirstName;
            existingEntity.LastName = string.IsNullOrWhiteSpace(form.LastName) ? existingEntity.LastName : form.LastName;
            existingEntity.Email = string.IsNullOrWhiteSpace(form.Email) ? existingEntity.Email : form.Email.ToLower();
            existingEntity.Phone = string.IsNullOrWhiteSpace(form.Phone) ? existingEntity.Phone : form.Phone;

            _projectManagerRepository.Update(existingEntity);

            await _projectManagerRepository.SaveAsync();
            await _projectManagerRepository.CommitTransactionAsync();

            var updatedPM = new ProjectManager
            {
                Id = existingEntity.Id,
                FirstName = existingEntity.FirstName,
                LastName = existingEntity.LastName,
                Email = existingEntity.Email,
                Phone = existingEntity.Phone
            };

            return updatedPM;
        }
        catch (Exception ex)
        {
            await _projectManagerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }
    public async Task<bool> DeleteProjectManagerAsync(int id)
    {
        try
        {
            var existingEntity = await _projectManagerRepository.GetAsync(pm => pm.Id == id);

            if (existingEntity == null)
            {
                Debug.WriteLine($"Project manager not found");
                return false;
            }

            await _projectManagerRepository.BeginTransactionAsync(); 

            _projectManagerRepository.Delete(existingEntity);


            await _projectManagerRepository.SaveAsync();
            await _projectManagerRepository.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex)
        {
            await _projectManagerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return false!;
        }

    }

}
