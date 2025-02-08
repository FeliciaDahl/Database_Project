using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationConsole;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Database_Project\\Data\\Data\\project_database.mdf;Integrated Security=True;Connect Timeout=30"),
    ServiceLifetime.Transient);
serviceCollection.AddScoped<IProjectService, ProjectService>();
serviceCollection.AddScoped<ICostumerService, CostumerService>();
serviceCollection.AddScoped<IProjectManagerService, ProjectManagerService>();
serviceCollection.AddScoped<IServiceService, ServiceService>();
serviceCollection.AddScoped<IStatusTypeService, StatusTypeService>();

serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<ICostumerRepository, CostumerRepository>();
serviceCollection.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
serviceCollection.AddScoped<IServiceRepository, ServiceRepository>();
serviceCollection.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
serviceCollection.AddScoped<IMenuDialog, MenuDialog>();

var serviceProvider = serviceCollection.BuildServiceProvider();


var menuDialog = serviceProvider.GetRequiredService<IMenuDialog>();


await menuDialog.ShowProjects();
