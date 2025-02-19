
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation_Maui.Services;
using System.Collections.ObjectModel;

namespace Presentation_Maui.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly ProjectApiService _projectApiService;
    public ObservableCollection<Project> Projects { get; } = [];
 

    public IAsyncRelayCommand LoadProjectsCommand { get; }

    public ListViewModel(ProjectApiService projectApiService)
    {
        _projectApiService = projectApiService;
        LoadProjectsCommand = new AsyncRelayCommand(LoadProjectsAsync);
        
        Task.Run(LoadProjectsAsync);
    }

   private async Task LoadProjectsAsync()
    {
        var projects = await _projectApiService.GetAllProjectsAsync();

        if(projects != null)
        {   
            Projects.Clear();
           
            foreach (var project in projects)
            {
                Projects.Add(project);
               
            }
        }

        
    }

    //private async Task SearchProjectsAsync(string searchInput)
    //{
    //    if (string.IsNullOrWhiteSpace(searchInput))
    //    {
           
    //        await LoadProjectsAsync();
    //        return;
    //    }

    //    var filteredProjects = await _projectApiService.GetProjectByIdAsync(searchInput);
    //    FilteredProjects.Clear();

    //    foreach (var project in filteredProjects)
    //    {
    //        FilteredProjects.Add(project);
    //    }
    //}

}
