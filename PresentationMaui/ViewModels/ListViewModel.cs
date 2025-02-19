
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Handlers.Items;
using PresentationMaui.Services;
using System.Collections.ObjectModel;

namespace PresentationMaui.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly ProjectApiService _projectApiService;

    public ObservableCollection<Project> Projects { get; } = new ObservableCollection<Project>();
 
    public ListViewModel(ProjectApiService projectApiService)
    {
        _projectApiService = projectApiService;

        Task.Run(LoadProjectsAsync);
    }



    [RelayCommand]
    private async Task LoadProjectsAsync()
    {
        var projects = await _projectApiService.GetAllProjectsAsync();

        if (projects != null)
        {


            foreach (var project in projects)
            {
                Projects.Add(project);
            }

        }
    }

    [RelayCommand]
    public async Task DeleteProject(Project project)
    {
        if (project != null)
        {
            bool success = await _projectApiService.DeleteProjectAsync(project.Id);
            if(success)
            {
                Projects.Remove(project);
            }
        }
    }

    [RelayCommand]
    private async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("AddPage");
    }
}


