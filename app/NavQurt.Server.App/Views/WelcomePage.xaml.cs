using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App.Views;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<WelcomeViewModel>();
    }
}
