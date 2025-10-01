using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.Models;

namespace NavQurt.Server.App.Services;

public class AuthService
{
    private readonly ApiClient _apiClient;
    private readonly TokenStorageService _tokenStorage;

    public AuthService(ApiClient apiClient, TokenStorageService tokenStorage)
    {
        _apiClient = apiClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var response = await _apiClient.PostAsync("/api/auth/login", request).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
            return AuthResult.FromError(error);
        }

        var apiResponse = await _apiClient.ReadAsAsync<ApiResponse<AuthResponse>>(response).ConfigureAwait(false);

        if (apiResponse?.Data is null || string.IsNullOrWhiteSpace(apiResponse.Data.AccessToken))
        {
            return AuthResult.FromError("Authentication response was empty.");
        }

        var jwtService = ServiceHelper.GetService<JwtService>();
        var role = jwtService.GetRoleFromToken(apiResponse.Data.AccessToken);

        await _tokenStorage.SaveTokensAsync(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken).ConfigureAwait(false);

        return AuthResult.FromSuccess(apiResponse.Data.AccessToken, apiResponse.Data.RefreshToken, role);
    }

    public async Task<ServiceResult> SignUpAsync(SignUpRequest request)
    {
        var response = await _apiClient.PostAsync("/api/auth/sign-up", request).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return ServiceResult.FromSuccess();
        }

        var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
        return ServiceResult.FromError(error);
    }

    public async Task<ServiceResult> SendCodeAsync(SendCodeRequest request)
    {
        var response = await _apiClient.PostAsync("/api/auth/send-code", request).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return ServiceResult.FromSuccess();
        }

        var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
        return ServiceResult.FromError(error);
    }

    public async Task<ServiceResult> ConfirmCodeAsync(ConfirmCodeRequest request)
    {
        var response = await _apiClient.PostAsync("/api/auth/confirm-code", request).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return ServiceResult.FromSuccess();
        }

        var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
        return ServiceResult.FromError(error);
    }

    public async Task<ServiceResult> RefreshTokenAsync(RefreshRequestDto request)
    {
        var response = await _apiClient.PutAsync("/api/auth/refresh-token", request, authorize: true).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var error = await _apiClient.ReadErrorAsync(response).ConfigureAwait(false);
            return ServiceResult.FromError(error);
        }

        var tokens = await _apiClient.ReadAsAsync<AuthResponse>(response).ConfigureAwait(false);
        if (tokens is not null)
        {
            await _tokenStorage.SaveTokensAsync(tokens.AccessToken, tokens.RefreshToken).ConfigureAwait(false);
        }

        return ServiceResult.FromSuccess();
    }

    public void LogOut()
    {
        _tokenStorage.ClearTokens();
    }
}
