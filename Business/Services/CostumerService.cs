
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

    public async Task<Costumer> CreateCostumerAsync(CostumerRegistrationForm form)
    {
      
        if (string.IsNullOrWhiteSpace(form.CostumerName))
            return null!;

        await _costumerRepository.BeginTransactionAsync();

        var costumerEntity = await _costumerRepository.GetAsync(c => c.CostumerName.ToLower() == form.CostumerName.Trim().ToLower());

        if (costumerEntity == null)
        {
            try
            {
                costumerEntity = CostumerFactory.Create(form);
               _costumerRepository.Add(costumerEntity);
                await _costumerRepository.SaveAsync();

                await _costumerRepository.CommitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _costumerRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error: {ex.Message}");
                return null!;
            }

        }
            return CostumerFactory.Create(costumerEntity);
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

    public async Task<Costumer> UpdateCostumerAsync(int id, CostumerUpdateForm form)
    {
        try
        {
            var existingEntity = await _costumerRepository.GetAsync(x => x.Id == id);

            if (existingEntity == null)
            {
                Debug.WriteLine("Costumer not found");
                return null!;
            }

            await _costumerRepository.BeginTransactionAsync();

           existingEntity.CostumerName = string.IsNullOrWhiteSpace(form.CostumerName) ? existingEntity.CostumerName : form.CostumerName;

            _costumerRepository.Update(existingEntity);

            await _costumerRepository.SaveAsync();
            await _costumerRepository.CommitTransactionAsync();

             var updatedCostumer = new Costumer
                    {
                        Id = existingEntity.Id,
                        CostumerName = existingEntity.CostumerName
                    };

            return updatedCostumer;
        }
        catch (Exception ex)
        {
           await  _costumerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return null!;
        }
       
    }
    public async Task<bool> DeleteCostumerAsync(int id)
    {
        try
        {
            var existingCostumer = await _costumerRepository.GetAsync(c => c.Id == id);

            if (existingCostumer == null)
            { 
                Debug.WriteLine("Costumer not found");
                return false;
            }

            await _costumerRepository.BeginTransactionAsync();

            _costumerRepository.Delete(existingCostumer);

            await _costumerRepository.SaveAsync();
            await _costumerRepository.CommitTransactionAsync(); 
            return true;
        }
        catch (Exception ex)
        {
            await _costumerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error: {ex.Message}");
            return false!;
        }

    }
}
