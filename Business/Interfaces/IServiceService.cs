using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<Service> CreateServiceAsync(ServiceRegistrationForm form);
        Task<bool> DeleteServiceAsync(int id);
        Task<Service> UpdateServiceAsync(ServiceUpdateForm form);
    }
}