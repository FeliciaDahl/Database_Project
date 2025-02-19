
using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CostumerFactory
{
    public static CostumerRegistrationForm Create()
    {
        return new CostumerRegistrationForm();
    }

    public static CostumerEntity Create(CostumerRegistrationForm form)
    {
        return new CostumerEntity
        {
            CostumerName = form.CostumerName
        };
    }

    public static Costumer Create(CostumerEntity entity)
    {
        return new Costumer
        {
            Id = entity.Id,
            CostumerName = entity.CostumerName
        };
    }

    
}
