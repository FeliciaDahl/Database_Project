
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace PresentationMaui.Services;

public class ServiceApiService
{
    private readonly HttpClient _httpClient;

    public ServiceApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Service?> AddCostumerAsync(ServiceRegistrationForm form)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7123/api/services", form);
        return await response.Content.ReadFromJsonAsync<Service>();
    }

    public async Task<Service?> UpdateServiceAsync(int id, ServiceUpdateForm form)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7123/api/services{id}", form);
        return await response.Content.ReadFromJsonAsync<Service>();
    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7123/api/services{id}");
        return response.IsSuccessStatusCode;
    }

}
