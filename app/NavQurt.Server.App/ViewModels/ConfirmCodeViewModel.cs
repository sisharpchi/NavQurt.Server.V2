using Microsoft.Maui.ApplicationModel;
using NavQurt.Server.App.Models;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.Utilities;

namespace NavQurt.Server.App.ViewModels;

public class ConfirmCodeViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    private string? _email;
    private string? _code;

    public ConfirmCodeViewModel(AuthService authService)
    {
        _authService = authService;
        ConfirmCodeCommand = new AsyncCommand(ConfirmCodeAsync);
    }

    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string? Code
    {
        get => _code;
        set => SetProperty(ref _code, value);
    }

    public AsyncCommand ConfirmCodeCommand { get; }

    private async Task ConfirmCodeAsync()
    {
        if (IsBusy)
        {
            return;
        }

        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Code))
        {
            ErrorMessage = "Verification code is required.";
            return;
        }

        try
        {
            IsBusy = true;
            var request = new ConfirmCodeRequest
            {
                Email = Email,
                Code = Code
            };

            var result = await _authService.ConfirmCodeAsync(request).ConfigureAwait(false);
            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage ?? "Failed to confirm verification code.";
                return;
            }

            await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync("//LoginPage")).ConfigureAwait(false);
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
}
