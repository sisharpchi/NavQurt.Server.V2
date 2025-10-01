using NavQurt.Server.App.Views;

namespace NavQurt.Server.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(ConfirmCodePage), typeof(ConfirmCodePage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
        Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
        Routing.RegisterRoute(nameof(RoleManagementPage), typeof(RoleManagementPage));

        this.Navigated += OnNavigated!;
    }

    private void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        if (CurrentPage is Page page)
        {
            Shell.SetBackButtonBehavior(page, new BackButtonBehavior
            {
                IsVisible = false,
                IsEnabled = false
            });
        }
    }
}
