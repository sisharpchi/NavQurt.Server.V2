namespace NavQurt.Server.App.Models;

public class ConfirmCodeRequest
{
    public string? Email { get; set; }
    public string? Code { get; set; }
}
