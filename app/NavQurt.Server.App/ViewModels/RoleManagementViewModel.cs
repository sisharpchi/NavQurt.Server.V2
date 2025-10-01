using Microsoft.Maui.ApplicationModel;
using NavQurt.Server.App.Models;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.Utilities;
using System;
using System.Collections.ObjectModel;

namespace NavQurt.Server.App.ViewModels;

public class RoleManagementViewModel : BaseViewModel
{
    private readonly RoleService _roleService;

    public RoleManagementViewModel(RoleService roleService)
    {
        _roleService = roleService;
        Roles = new ObservableCollection<RoleDto>();
        LoadRolesCommand = new AsyncCommand(LoadRolesAsync);
    }

    public ObservableCollection<RoleDto> Roles { get; }

    public AsyncCommand LoadRolesCommand { get; }

    private async Task LoadRolesAsync()
    {
        if (IsBusy)
        {
            return;
        }

        ErrorMessage = null;

        try
        {
            IsBusy = true;
            await MainThread.InvokeOnMainThreadAsync(Roles.Clear);
            var result = await _roleService.GetRolesAsync().ConfigureAwait(false);
            if (!result.Success)
            {
                ErrorMessage = result.ErrorMessage;
                return;
            }

            var roles = result.Data ?? Array.Empty<RoleDto>();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach (var role in roles)
                {
                    Roles.Add(role);
                }
            }).ConfigureAwait(false);
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
