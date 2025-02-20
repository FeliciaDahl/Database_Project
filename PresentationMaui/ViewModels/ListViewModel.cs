
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Handlers.Items;
using PresentationMaui.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

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
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7123/api/projects");

            if (response.IsSuccessStatusCode)
            {
                var projectList = await response.Content.ReadFromJsonAsync<List<Project>>();
                Debug.WriteLine($"Fetched {projectList?.Count} projects from API");

                if (projectList != null)
                {
                    // Uppdatera ObservableCollection på UI-tråden
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Projects.Clear();
                        foreach (var project in projectList)
                        {
                            Console.WriteLine($"Projekt: {project.Title}, Kund: {project.CostumerName}");
                            Projects.Add(project);
                        }
                    });
                }
            }
            else
            {
                Debug.WriteLine($"Failed to load projects. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching projects: {ex.Message}");
        }
    }

    //[RelayCommand]
    //private async Task LoadProjectsAsync()
    //{
    //    var projects = await _projectApiService.GetAllProjectsAsync();

    //    if (projects != null)
    //    {
    //        Console.WriteLine($"Hämtade {projects.Count()} projekt.");

    //        foreach (var project in projects)
    //        {
    //            Projects.Add(project);
    //        }

    //    }
    //}

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

    //[RelayCommand]
    //private async Task NavigateToAdd()
    //{
    //    await Shell.Current.GoToAsync("AddPage");
    //}
}


