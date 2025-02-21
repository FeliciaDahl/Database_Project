using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<IEnumerable<ProjectEntity>> GetAllAsync(Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null);
        Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> predicate, Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>>? includeExpression = null);
    }
}