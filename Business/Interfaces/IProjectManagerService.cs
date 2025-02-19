using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IProjectManagerService
    {
        Task<bool> DeleteProjectManagerAsync(int id);
        Task<IEnumerable<ProjectManager>> GetAllProjectManagersAsync();
        Task<ProjectManager> CreateProjectManagerAsync(ProjectManagerRegistrationForm form);
        Task<ProjectManager?> GetProjectManagerAsync(Expression<Func<ProjectManagerEntity, bool>> expression, Func<IQueryable<ProjectManagerEntity>, IQueryable<ProjectManagerEntity>>? includeExpression = null);
        Task<ProjectManager> UpdateProjectManagerAsync(int id, ProjectManagerUpdateForm form);
    }
}