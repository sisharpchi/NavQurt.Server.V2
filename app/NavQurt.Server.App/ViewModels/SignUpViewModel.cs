using Microsoft.Maui.ApplicationModel;
using NavQurt.Server.App.Models;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.Utilities;
using NavQurt.Server.App.Views;

namespace NavQurt.Server.App.ViewModels;

public class SignUpViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    private string? _firstName;
    private string? _lastName;
    private string? _userName;
    private string? _email;
    private string? _password;
    private string? _phoneNumber;

    public SignUpViewModel(AuthService authService)
    {
        _authService = authService;

        SignUpCommand = new AsyncCommand(SignUpAsync);
        NavigateToLoginCommand = new AsyncCommand(NavigateToLoginAsync);
    }

    public string? FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string? LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string? UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string? Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string? PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public AsyncCommand SignUpCommand { get; }

    public AsyncCommand NavigateToLoginCommand { get; }

    private async Task SignUpAsync()
    {
        if (IsBusy)
        {
            return;
        }

        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = "Email, username, and password are required.";
            return;
        }

        try
        {
            IsBusy = true;

            var request = new SignUpRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber
            };

            var signUpResult = await _authService.SignUpAsync(request).ConfigureAwait(false);
            if (!signUpResult.Success)
            {
                ErrorMessage = signUpResult.ErrorMessage ?? "Sign-up failed.";
                return;
            }

            var sendCodeResult = await _authService.SendCodeAsync(new SendCodeRequest { Email = Email }).ConfigureAwait(false);
            if (!sendCodeResult.Success)
            {
                ErrorMessage = sendCodeResult.ErrorMessage ?? "Failed to send verification code.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(Email))
            {
                var route = $"{nameof(ConfirmCodePage)}?email={Uri.EscapeDataString(Email)}";
                await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(route)).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task NavigateToLoginAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync("..", true)).ConfigureAwait(false);
    }
}
