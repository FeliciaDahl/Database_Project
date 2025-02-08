
using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class CostumerService : ICostumerService
{
    private readonly ICostumerRepository _costumerRepository;

    public CostumerService(ICostumerRepository costumerRepository)
    {
        _costumerRepository = costumerRepository;
    }

    public async Task<CostumerEntity> GetOrCreateCostumerAsync(CostumerRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.CostumerName))
            return null!;

        var costumer = await _costumerRepository.GetAsync(c => c.CostumerName.ToLower() == form.CostumerName.Trim().ToLower());

        if (costumer == null)
        {
            try
            {
                costumer = CostumerFactory.Create(form);
                return await _costumerRepository.CreateAsync(costumer);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null!;
            }

        }
        return costumer;
    }

    public async Task<IEnumerable<Costumer>> GetAllCostumersAsync()
    {
        var costumerEntity = await _costumerRepository.GetAllAsync();
        return costumerEntity.Select(entity => CostumerFactory.Create(entity));
    }


    public async Task<Costumer?> GetCostumerAsync(Expression<Func<CostumerEntity, bool>> expression, Func<IQueryable<CostumerEntity>, IQueryable<CostumerEntity>>? includeExpression = null)
    {
        var costumerEntity = await _costumerRepository.GetAsync(expression, includeExpression);

        if (costumerEntity == null)
        {
            return null;
        }

        return CostumerFactory.Create(costumerEntity);
    }

    public async Task<Costumer> UpdateCostumerAsync(CostumerUpdateForm form)
    {
        try
        {
            var existingEntity = await _costumerRepository.GetAsync(c => c.Id == form.Id);

            if (existingEntity == null)
            {
                return null!;
            }


            existingEntity.CostumerName = form.CostumerName;

            var result = await _costumerRepository.UpdateAsync(c => c.Id == form.Id, existingEntity);

            if (result == null)
            {
                return null!;
            }


            return new Costumer
            {
                Id = result.Id,
                CostumerName = result.CostumerName
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ett fel uppstod vid uppdatering: {ex.Message}");
            return null!;
        }

    }

    public async Task<bool> DeleteCostumerAsync(int id)
    {
        try
        {
            var result = await _costumerRepository.DeleteAsync(p => p.Id == id);
            if (!result)
            {
                Debug.WriteLine($"Could not delete costumer {id}.");
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
