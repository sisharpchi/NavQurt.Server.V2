namespace NavQurt.Server.App.Models;

public class RefreshRequestDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
