using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App.Views;

public partial class RoleManagementPage : ContentPage
{
    private readonly RoleManagementViewModel _viewModel;

    public RoleManagementPage()
    {
        InitializeComponent();
        _viewModel = ServiceHelper.GetService<RoleManagementViewModel>();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.Roles.Count == 0)
        {
            _viewModel.LoadRolesCommand.Execute(null);
        }
    }
}
