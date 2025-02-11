
using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<Service> CreateServiceAsync(ServiceRegistrationForm form)
    {
        await _serviceRepository.BeginTransactionAsync();

        if (string.IsNullOrWhiteSpace(form.ServiceName)) 
            return null!;

        try
        {
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

    public async Task<Service> UpdateServiceAsync(ServiceUpdateForm form)
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

            var updatedEntity = ServiceFactory.Create(form);

            _serviceRepository.Update(updatedEntity);

            await _serviceRepository.SaveAsync();
            await _serviceRepository.CommitTransactionAsync();

            return ServiceFactory.Create(updatedEntity);
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

            _serviceRepository.Update(existingEntity);

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
