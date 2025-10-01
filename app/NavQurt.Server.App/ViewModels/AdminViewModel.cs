using Microsoft.Maui.ApplicationModel;
using NavQurt.Server.App.Models;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.Utilities;
using NavQurt.Server.App.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace NavQurt.Server.App.ViewModels;

public class AdminViewModel : BaseViewModel
{
    private readonly AdminService _adminService;

    private string? _roleFilter;
    private string? _statusMessage;

    public AdminViewModel(AdminService adminService)
    {
        _adminService = adminService;

        Users = new ObservableCollection<UserDto>();
        LoadUsersCommand = new AsyncCommand(LoadUsersAsync);
        DeleteUserCommand = new AsyncCommand<UserDto>(DeleteUserAsync);
        UpdateUserRoleCommand = new AsyncCommand<UserDto>(UpdateUserRoleAsync);
        NavigateToRolesCommand = new AsyncCommand(NavigateToRolesAsync);
    }

    public ObservableCollection<UserDto> Users { get; }

    public string? RoleFilter
    {
        get => _roleFilter;
        set => SetProperty(ref _roleFilter, value);
    }

    public string? StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (SetProperty(ref _statusMessage, value))
            {
                OnPropertyChanged(nameof(HasStatus));
            }
        }
    }

    public bool HasStatus => !string.IsNullOrWhiteSpace(StatusMessage);

    public AsyncCommand LoadUsersCommand { get; }
    public AsyncCommand<UserDto> DeleteUserCommand { get; }
    public AsyncCommand<UserDto> UpdateUserRoleCommand { get; }
    public AsyncCommand NavigateToRolesCommand { get; }

    private async Task LoadUsersAsync()
    {
        if (IsBusy)
        {
            return;
        }

        ErrorMessage = null;
        StatusMessage = null;

        if (string.IsNullOrWhiteSpace(RoleFilter))
        {
            ErrorMessage = "Enter a role to filter users.";
            return;
        }

        try
        {
            IsBusy = true;
            await MainThread.InvokeOnMainThreadAsync(Users.Clear);
            var result = await _adminService.GetUsersByRoleAsync(RoleFilter).ConfigureAwait(false);
            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return;
            }

            var users = result.Data ?? Enumerable.Empty<UserDto>();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }).ConfigureAwait(false);
            StatusMessage = Users.Count == 0 ? "No users found." : $"Loaded {Users.Count} users.";
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

    private async Task DeleteUserAsync(UserDto? user)
    {
        if (user is null)
        {
            return;
        }

        try
        {
            var result = await _adminService.DeleteUserAsync(user.Id).ConfigureAwait(false);
            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return;
            }

            await MainThread.InvokeOnMainThreadAsync(() => Users.Remove(user)).ConfigureAwait(false);
            StatusMessage = "User deleted.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task UpdateUserRoleAsync(UserDto? user)
    {
        if (user is null || string.IsNullOrWhiteSpace(user.NewRole))
        {
            ErrorMessage = "Enter a new role before updating.";
            return;
        }

        try
        {
            var result = await _adminService.UpdateUserRoleAsync(user.Id, user.NewRole).ConfigureAwait(false);
            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return;
            }

            user.Role = user.NewRole;
            user.NewRole = string.Empty;
            StatusMessage = "Role updated successfully.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task NavigateToRolesAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(nameof(RoleManagementPage))).ConfigureAwait(false);
    }
}
