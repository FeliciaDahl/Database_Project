using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ICostumerService
    {
        Task<bool> DeleteCostumerAsync(int id);
        Task<IEnumerable<Costumer>> GetAllCostumersAsync();
        Task<Costumer?> GetCostumerAsync(Expression<Func<CostumerEntity, bool>> expression, Func<IQueryable<CostumerEntity>, IQueryable<CostumerEntity>>? includeExpression = null);
        Task<CostumerEntity> GetOrCreateCostumerAsync(CostumerRegistrationForm form);
        Task<Costumer> UpdateCostumerAsync(CostumerUpdateForm form);
    }
}