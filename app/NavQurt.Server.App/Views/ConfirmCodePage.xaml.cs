using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App.Views;

public partial class ConfirmCodePage : ContentPage, IQueryAttributable
{
    public ConfirmCodePage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<ConfirmCodeViewModel>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is ConfirmCodeViewModel viewModel && query.TryGetValue("email", out var emailObj) && emailObj is string email)
        {
            viewModel.Email = Uri.UnescapeDataString(email);
        }
    }
}
