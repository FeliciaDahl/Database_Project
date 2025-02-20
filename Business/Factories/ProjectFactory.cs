
using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Net.NetworkInformation;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();
  

    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CostumerId = form.CostumerId,
            ProjectManagerId = form.ProjectManagerId,
            ServiceId = form.ServiceId,
            StatusTypeId = form.StatusTypeId,
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        return new Project
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CostumerId = entity.Costumer.Id,
            ProjectManagerId = entity.ProjectManager.Id,
            ServiceId = entity.Service.Id,
            StatusTypeId = entity.StatusType.Id,
            CostumerName = entity.Costumer.CostumerName,
            ProjectManagerFirstName = entity.ProjectManager.FirstName,
            ProjectManagerLastName = entity.ProjectManager.LastName, 
            ProjectManagerEmail = entity.ProjectManager.Email,
            ServiceName = entity.Service.ServiceName,
            ServiceDescription = entity.Service.ServiceDescription!,
            ServicePrice = entity.Service.Price,
            StatusTypeName = entity.StatusType.StatusName
        };
    }
    public static ProjectUpdateForm Create(Project project)
    {
        return new ProjectUpdateForm
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            CostumerId = project.CostumerId,
            ProjectManagerId = project.ProjectManagerId,
            ServiceId = project.ServiceId,
            StatusTypeId = project.StatusTypeId,
        };
    }
    public static ProjectEntity Create(ProjectUpdateForm form) 
    {
        return new ProjectEntity
        {
            Id = form.Id,
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CostumerId = form.CostumerId,
            ProjectManagerId = form.ProjectManagerId,
            ServiceId = form.ServiceId,
            StatusTypeId = form.StatusTypeId,
        };
            
    }
}
