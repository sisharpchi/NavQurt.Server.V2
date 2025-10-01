using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App.Views;

public partial class AdminPage : ContentPage
{
    public AdminPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<AdminViewModel>();
    }
}
