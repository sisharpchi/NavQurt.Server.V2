namespace NavQurt.Server.App.Models;

public class AuthResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? TokenType { get; set; }
    public int Expires { get; set; }
}
