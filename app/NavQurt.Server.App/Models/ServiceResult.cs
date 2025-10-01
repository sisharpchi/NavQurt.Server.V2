namespace NavQurt.Server.App.Models;

public class ServiceResult
{
    public bool Success { get; }
    public string? ErrorMessage { get; }

    public ServiceResult(bool success, string? errorMessage)
    {
        Success = success;
        ErrorMessage = string.IsNullOrWhiteSpace(errorMessage) ? null : errorMessage;
    }

    public static ServiceResult FromSuccess() => new(true, null);

    public static ServiceResult FromError(string? errorMessage) => new(false, string.IsNullOrWhiteSpace(errorMessage) ? "An unexpected error occurred." : errorMessage);
}

public class AuthResult : ServiceResult
{
    public string? AccessToken { get; }
    public string? RefreshToken { get; }
    public string? Role { get; }

    public AuthResult(bool success, string? errorMessage, string? accessToken, string? refreshToken, string? role = null)
        : base(success, errorMessage)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Role = role;
    }

    public static AuthResult FromSuccess(string? accessToken, string? refreshToken, string? role = null) => new(true, null, accessToken, refreshToken, role);

    public static AuthResult FromError(string? errorMessage) => new(false, string.IsNullOrWhiteSpace(errorMessage) ? "An unexpected error occurred." : errorMessage, null, null);
}

public class DataResult<T> : ServiceResult
{
    public T? Data { get; }

    public DataResult(bool success, string? errorMessage, T? data)
        : base(success, errorMessage)
    {
        Data = data;
    }

    public static DataResult<T> FromSuccess(T? data) => new(true, null, data);

    public static DataResult<T> FromError(string? errorMessage) => new(false, string.IsNullOrWhiteSpace(errorMessage) ? "An unexpected error occurred." : errorMessage, default);
}
