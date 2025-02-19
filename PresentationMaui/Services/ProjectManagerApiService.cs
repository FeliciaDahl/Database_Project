
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace PresentationMaui.Services;

public class ProjectManagerApiService
{
    private readonly HttpClient _httpClient;

    public ProjectManagerApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProjectManager?> AddProjectManagerAsync(ProjectManagerRegistrationForm form)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7123/api/projectmanagers", form);
        return await response.Content.ReadFromJsonAsync<ProjectManager>();
    }

    public async Task<IEnumerable<ProjectManager>?> GetAllProjectManagerAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProjectManager>>("https://localhost:7123/api/projectmanagers");
    }

    public async Task<ProjectManager?> GetProjectManagerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProjectManager>($"https://your-api-url.com/api/projectmanagers/{id}");
    }

    public async Task<ProjectManager?> UpdateProjectManagerAsync(int id, ProjectManagerUpdateForm form)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://your-api-url.com/api/projectmanagers/{id}", form);
        return await response.Content.ReadFromJsonAsync<ProjectManager>();
    }

    public async Task<bool> DeleteProjectManagerAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://your-api-url.com/api/projectmanagers/{id}");
        return response.IsSuccessStatusCode;
    }

}