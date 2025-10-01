using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using NavQurt.Server.App.Models;

namespace NavQurt.Server.App.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly TokenStorageService _tokenStorage;
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiClient(HttpClient httpClient, TokenStorageService tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<HttpResponseMessage> GetAsync(string uri, bool authorize = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendAsync(request, authorize).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string uri, T payload, bool authorize = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = JsonContent.Create(payload, options: _serializerOptions)
        };
        return await SendAsync(request, authorize).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string uri, T payload, bool authorize = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = JsonContent.Create(payload, options: _serializerOptions)
        };
        return await SendAsync(request, authorize).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> PatchAsync(string uri, bool authorize = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch, uri);
        return await SendAsync(request, authorize).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string uri, bool authorize = false)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await SendAsync(request, authorize).ConfigureAwait(false);
    }

    public async Task<T?> ReadAsAsync<T>(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<T>(_serializerOptions).ConfigureAwait(false);
    }

    public async Task<string?> ReadErrorAsync(HttpResponseMessage response)
    {
        var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return string.IsNullOrWhiteSpace(message) ? response.ReasonPhrase : message;
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool authorize)
    {
        if (authorize)
        {
            var token = await _tokenStorage.GetAccessTokenAsync().ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await _httpClient.SendAsync(request).ConfigureAwait(false);
    }
}
