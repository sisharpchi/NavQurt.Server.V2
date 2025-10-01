namespace NavQurt.Server.App.ViewModels;

public class WelcomeViewModel : BaseViewModel
{
    private string _welcomeMessage = "Welcome to the app";

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set => SetProperty(ref _welcomeMessage, value);
    }
}
