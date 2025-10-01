using Microsoft.Extensions.Logging;
using NavQurt.Server.App.Helpers;
using NavQurt.Server.App.Services;
using NavQurt.Server.App.ViewModels;

namespace NavQurt.Server.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiConstants.BaseUrl)
                };
                return client;
            });

            builder.Services.AddSingleton<TokenStorageService>();
            builder.Services.AddSingleton<JwtService>();
            builder.Services.AddSingleton<ApiClient>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<AdminService>();
            builder.Services.AddSingleton<RoleService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<ConfirmCodeViewModel>();
            builder.Services.AddTransient<AdminViewModel>();
            builder.Services.AddTransient<RoleManagementViewModel>();
            builder.Services.AddTransient<WelcomeViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            ServiceHelper.Services = app.Services;
            return app;
        }
    }
}
