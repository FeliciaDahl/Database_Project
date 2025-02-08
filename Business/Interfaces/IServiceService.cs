using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceEntity> CreateServiceAsync(ServiceRegistrationForm form);
        Task<bool> DeleteServiceAsync(int id);
        Task<Service> UpdateServiceAsync(ServiceUpdateForm form);
    }
}