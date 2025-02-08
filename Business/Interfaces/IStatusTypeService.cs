using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IStatusTypeService
    {
        Task<IEnumerable<StatusTypeEntity>> GetAllServicesAsync();
        Task<StatusType> GetStatusTypeByIdAsync(int id);
    }
}