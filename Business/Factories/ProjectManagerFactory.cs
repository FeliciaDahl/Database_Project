
using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectManagerFactory
{
    public static ProjectManagerRegistrationForm Create()
    {
        return new ProjectManagerRegistrationForm();
    }

    public static ProjectManagerEntity Create(ProjectManagerRegistrationForm form)
    {
        return new ProjectManagerEntity
        {
           
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email.ToLower(),
            Phone = form.Phone
        };
    }

    public static ProjectManager Create(ProjectManagerEntity entity)
    {
        return new ProjectManager
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone
        };
    }

    public static ProjectManagerUpdateForm Create(ProjectManager pm)
    {
        return new ProjectManagerUpdateForm
        {
            
            FirstName = pm.FirstName,
            LastName = pm.LastName,
            Email = pm.Email,
            Phone = pm.Phone

        };
    }

    public static ProjectManagerEntity Create(ProjectManagerUpdateForm form)
    {
        return new ProjectManagerEntity
        {
           
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            Phone = form.Phone
        };
    }
}
