using Microsoft.Maui.Storage;

namespace NavQurt.Server.App.Services;

public class TokenStorageService
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    public async Task SaveTokensAsync(string? accessToken, string? refreshToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            SecureStorage.Default.Remove(AccessTokenKey);
        }
        else
        {
            await SecureStorage.Default.SetAsync(AccessTokenKey, accessToken);
        }

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            SecureStorage.Default.Remove(RefreshTokenKey);
        }
        else
        {
            await SecureStorage.Default.SetAsync(RefreshTokenKey, refreshToken);
        }
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            return await SecureStorage.Default.GetAsync(AccessTokenKey);
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        try
        {
            return await SecureStorage.Default.GetAsync(RefreshTokenKey);
        }
        catch
        {
            return null;
        }
    }

    public void ClearTokens()
    {
        SecureStorage.Default.Remove(AccessTokenKey);
        SecureStorage.Default.Remove(RefreshTokenKey);
    }
}
