using NavQurt.Server.App.Models;

namespace NavQurt.Server.App.Services;

public class RoleService
{
    private readonly ApiClient _apiClient;

    public RoleService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<DataResult<IEnumerable<RoleDto>>> GetRolesAsync()
    {
        var response = await _apiClient.GetAsync("/api/role/get-all", authorize: true).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
            return DataResult<IEnumerable<RoleDto>>.FromError(error);
        }

        var roles = await _apiClient.ReadAsAsync<IEnumerable<RoleDto>>(response).ConfigureAwait(false);
        return DataResult<IEnumerable<RoleDto>>.FromSuccess(roles);
    }
}
