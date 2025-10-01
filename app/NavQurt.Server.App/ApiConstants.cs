namespace NavQurt.Server.App;

public static class ApiConstants
{
#if ANDROID
    public const string BaseUrl = "https://10.0.2.2:5000"; // Android Emulator
#elif WINDOWS
    public const string BaseUrl = "https://localhost:5000"; // Windows/Web
#else
    public const string BaseUrl = "https://localhost:5000"; // default
#endif
}
