using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<LoginViewModel>();
    }
}
