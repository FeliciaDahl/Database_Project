
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

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<Service> CreateServiceAsync(ServiceRegistrationForm form)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(form.ServiceName))
                return null!;

            await _serviceRepository.BeginTransactionAsync();

            var serviceEntity = ServiceFactory.Create(form);
            _serviceRepository.Add(serviceEntity);

            await _serviceRepository.SaveAsync();
            await _serviceRepository.CommitTransactionAsync();

            return ServiceFactory.Create(serviceEntity);
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        var serviceEntity = await _serviceRepository.GetAllAsync();
        return serviceEntity.Select(entity => ServiceFactory.Create(entity));
    }


    public async Task<Service?> GetServiceAsync(Expression<Func<ServiceEntity, bool>> expression, Func<IQueryable<ServiceEntity>, IQueryable<ServiceEntity>>? includeExpression = null)
    {
        var serviceEntity = await _serviceRepository.GetAsync(expression, includeExpression);

        if (serviceEntity == null)
        {
            return null;
        }

        return ServiceFactory.Create(serviceEntity);
    }

    public async Task<Service> UpdateServiceAsync(int id, ServiceUpdateForm form)
    {
        try
        {
            var existingEntity = await _serviceRepository.GetAsync(s => s.Id == s.Id);

            if (existingEntity == null)
            {
                Debug.WriteLine("Service not found");
                return null!;
            }

            await _serviceRepository.BeginTransactionAsync();

            existingEntity.ServiceName = string.IsNullOrWhiteSpace(form.ServiceName) ? existingEntity.ServiceName : form.ServiceName;
            existingEntity.ServiceDescription = string.IsNullOrWhiteSpace(form.ServiceDescription) ? existingEntity.ServiceDescription : form.ServiceDescription;
            existingEntity.Price = form.Price != default ? form.Price : existingEntity.Price;

            _serviceRepository.Update(existingEntity);

            await _serviceRepository.SaveAsync();
            await _serviceRepository.CommitTransactionAsync();

            var updatedService = new Service
            {
                Id = existingEntity.Id,
                ServiceName = existingEntity.ServiceName,
                ServiceDescription = existingEntity.ServiceDescription,
                Price = existingEntity.Price
            };

            return updatedService;
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        try
        {
            var existingEntity = await _serviceRepository.GetAsync(s => s.Id == s.Id);

            if (existingEntity == null)
            {
                Debug.WriteLine("Service not found");
                return false!;
            }

            await _serviceRepository.BeginTransactionAsync();

            _serviceRepository.Delete(existingEntity);

            await _serviceRepository.SaveAsync();
            await _serviceRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return false!;
        }

    }
}
