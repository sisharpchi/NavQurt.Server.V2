using Microsoft.Extensions.DependencyInjection;

namespace NavQurt.Server.App.Helpers;

public static class ServiceHelper
{
    public static IServiceProvider? Services { get; set; }

    public static T GetService<T>() where T : notnull
    {
        if (Services is null)
        {
            throw new InvalidOperationException("Service provider is not initialized.");
        }

        return Services.GetRequiredService<T>();
    }
}
