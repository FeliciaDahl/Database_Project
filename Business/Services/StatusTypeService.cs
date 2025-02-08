
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class StatusTypeService : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository;

    public StatusTypeService(IStatusTypeRepository statusTypeRepository)
    {
        _statusTypeRepository = statusTypeRepository;
    }

    public async Task<IEnumerable<StatusTypeEntity>> GetAllServicesAsync()
    {
        return await _statusTypeRepository.GetAllAsync();

    }

    public async Task<StatusType> GetStatusTypeByIdAsync(int id)
    {
        var statusEntity = await _statusTypeRepository.GetAsync(st => st.Id == id);

        if (statusEntity == null)
            return null!;

        return new StatusType { StatusName = statusEntity.StatusName };
    }
}
