using Microsoft.Maui.ApplicationModel;
using NavQurt.Server.App.Models;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.Utilities;
using NavQurt.Server.App.Views;

namespace NavQurt.Server.App.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;

    private string? _userName;
    private string? _password;

    public LoginViewModel(AuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;

        LoginCommand = new AsyncCommand(LoginAsync);
        NavigateToSignUpCommand = new AsyncCommand(NavigateToSignUpAsync);
    }

    public string? UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string? Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public AsyncCommand LoginCommand { get; }

    public AsyncCommand NavigateToSignUpCommand { get; }

    private async Task LoginAsync()
    {
        if (IsBusy)
        {
            return;
        }

        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Username and password are required.";
            return;
        }

        try
        {
            IsBusy = true;

            var request = new LoginRequest
            {
                UserName = UserName,
                Password = Password
            };

            var result = await _authService.LoginAsync(request).ConfigureAwait(false);

            if (!result.Success || string.IsNullOrWhiteSpace(result.AccessToken))
            {
                ErrorMessage = result.ErrorMessage ?? "Login failed.";
                return;
            }

            var role = _jwtService.GetRoleFromToken(result.AccessToken);
            if (string.IsNullOrWhiteSpace(role))
            {
                role = result.Role;
            }

            var targetRoute = string.Equals(role, "SuperAdmin", StringComparison.OrdinalIgnoreCase) ||
                              string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase)
                ? $"{nameof(AdminPage)}"
                : $"{nameof(WelcomePage)}";

            await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(targetRoute)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
            Password = string.Empty;
        }
    }

    private async Task NavigateToSignUpAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(nameof(SignUpPage))).ConfigureAwait(false);
    }
}
