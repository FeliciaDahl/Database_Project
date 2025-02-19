using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Presentation_Maui.Services;
using Presentation_Maui.ViewModels;

namespace Presentation_Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpClient>(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7123/") });
            builder.Services.AddSingleton<ProjectApiService>();
            builder.Services.AddSingleton<CostumerApiService>();
            builder.Services.AddSingleton<ProjectManagerApiService>();
            builder.Services.AddSingleton<ServiceApiService>();
            builder.Services.AddSingleton<StatusTypeApiService>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ICostumerService, CostumerService>();
            builder.Services.AddScoped<IProjectManagerService, ProjectManagerService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ICostumerRepository, CostumerRepository>();
            builder.Services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
