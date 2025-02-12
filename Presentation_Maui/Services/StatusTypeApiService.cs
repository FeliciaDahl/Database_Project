
using Business.Dto;
using Business.Models;
using System.Net.Http.Json;

namespace Presentation_Maui.Services;

public class StatusTypeApiService
{
    private readonly HttpClient _httpClient;

    public StatusTypeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<StatusType>?> GetAllStatusTypeAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<StatusType>>("https://localhost:7123/api/status");
    }

    public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<StatusType>($"https://your-api-url.com/api/status/{id}");
    }

}
