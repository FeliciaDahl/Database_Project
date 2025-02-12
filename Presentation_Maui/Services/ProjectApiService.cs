
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace Presentation_Maui.Services;

public class ProjectApiService
{
    private readonly HttpClient _httpClient;

    public ProjectApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

   public async Task<Project?> AddProjectAsync(ProjectRegistrationForm form)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7123/api/projects", form);
        return await response.Content.ReadFromJsonAsync<Project>();
    }

}
