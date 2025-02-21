using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<Service> CreateServiceAsync(ServiceRegistrationForm form);
        Task<bool> DeleteServiceAsync(int id);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Func<IQueryable<ServiceEntity>, IQueryable<ServiceEntity>>? includeExpression = null);
        Task<Service> UpdateServiceAsync(int id, ServiceUpdateForm form);
    }
}