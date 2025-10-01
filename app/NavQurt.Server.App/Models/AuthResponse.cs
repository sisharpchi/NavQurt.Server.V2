namespace NavQurt.Server.App.Models;

public class AuthResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? Role { get; set; }
}
