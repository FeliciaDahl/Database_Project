
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

    public async Task<ServiceEntity> CreateServiceAsync(ServiceRegistrationForm form)
    {
        if (form == null)
            return null!;

        try
        {
            var service = ServiceFactory.Create(form);
            return await _serviceRepository.CreateAsync(service);

        }
        catch (Exception ex)
        {
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
                return null!;

            }

            var updatedEntity = ServiceFactory.Create(form);

            var result = await _serviceRepository.UpdateAsync(s => s.Id == form.Id, updatedEntity);
            if (result == null)
            {
                return null!;
            }


            return ServiceFactory.Create(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }

    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        try
        {
            var result = await _serviceRepository.DeleteAsync(s => s.Id == id);
            if (!result)
            {
                Debug.WriteLine($"Could not delete service {id}.");
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
