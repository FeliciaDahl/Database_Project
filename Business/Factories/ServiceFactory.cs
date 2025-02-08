
using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static ServiceRegistrationForm Create()
    {
        return new ServiceRegistrationForm();
    }

    public static ServiceEntity Create(ServiceRegistrationForm form)
    {
        return new ServiceEntity
        {
            ServiceName = form.ServiceName,
            ServiceDescription = form.ServiceDescription,
            Price = form.Price
        };
    }

    public static Service Create(ServiceEntity entity)
    {
        return new Service
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            ServiceDescription = entity.ServiceDescription,
            Price = entity.Price
        };
    }

    public static ServiceUpdateForm Create(Service service)
    {
        return new ServiceUpdateForm
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            ServiceDescription = service.ServiceDescription,
            Price = service.Price

        };
    }

    public static ServiceEntity Create(ServiceUpdateForm form)
    {
        return new ServiceEntity
        {
            Id = form.Id,
            ServiceName = form.ServiceName,
            ServiceDescription = form.ServiceDescription,
            Price = form.Price
        };
    }

}
