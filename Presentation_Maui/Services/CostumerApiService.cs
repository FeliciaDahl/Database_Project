
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace Presentation_Maui.Services;

public class CostumerApiService
{
    private readonly HttpClient _httpClient;

    public CostumerApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Costumer?> AddCostumerAsync(CostumerRegistrationForm form)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7123/api/costumers", form);
        return await response.Content.ReadFromJsonAsync<Costumer>();
    }

    public async Task<IEnumerable<Costumer>?> GetAllCostumerAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Costumer>>("https://localhost:7123/api/costumers");
    }

    public async Task<Costumer?> GetCostumerByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Costumer>($"https://your-api-url.com/api/costumers/{id}");
    }

    public async Task<Costumer?> UpdateCostumerAsync(int id, CostumerUpdateForm form)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://your-api-url.com/api/costumers/{id}", form);
        return await response.Content.ReadFromJsonAsync<Costumer>();
    }

    public async Task<bool> DeleteCostumerAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://your-api-url.com/api/costumers/{id}");
        return response.IsSuccessStatusCode;
    }

}
