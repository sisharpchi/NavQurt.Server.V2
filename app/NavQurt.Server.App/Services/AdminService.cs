using NavQurt.Server.App.Models;

namespace NavQurt.Server.App.Services;

public class AdminService
{
    private readonly ApiClient _apiClient;

    public AdminService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<DataResult<IEnumerable<UserDto>>> GetUsersByRoleAsync(string role)
    {
        var response = await _apiClient.GetAsync($"/api/admin/get-all-users-by-role?role={Uri.EscapeDataString(role)}", authorize: true).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
            return DataResult<IEnumerable<UserDto>>.FromError(error);
        }

        var users = await _apiClient.ReadAsAsync<IEnumerable<UserDto>>(response).ConfigureAwait(false);
        return DataResult<IEnumerable<UserDto>>.FromSuccess(users);
    }

    public async Task<ServiceResult> DeleteUserAsync(long userId)
    {
        var response = await _apiClient.DeleteAsync($"/api/admin/delete-user-by-id?userId={userId}", authorize: true).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return ServiceResult.FromSuccess();
        }

        var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
        return ServiceResult.FromError(error);
    }

    public async Task<ServiceResult> UpdateUserRoleAsync(long userId, string newRole)
    {
        var response = await _apiClient.PatchAsync($"/api/admin/update-role?userId={userId}&userRole={Uri.EscapeDataString(newRole)}", authorize: true).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return ServiceResult.FromSuccess();
        }

        var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
        return ServiceResult.FromError(error);
    }
}
