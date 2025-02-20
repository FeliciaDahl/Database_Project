
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace PresentationMaui.Services;

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

        public async Task<IEnumerable<Project>?> GetAllProjectsAsync()
        {
           return await _httpClient.GetFromJsonAsync<IEnumerable<Project>>("https://localhost:7123/api/projects");

        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Project>($"https://localhost:7123/api/projects{id}");
        }

        public async Task<Project?> UpdateProjectAsync(int id, ProjectUpdateForm form)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7123/api/projects{id}", form);
            return await response.Content.ReadFromJsonAsync<Project>();
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7123/api/projects{id}");
            return response.IsSuccessStatusCode;
        }
    }
