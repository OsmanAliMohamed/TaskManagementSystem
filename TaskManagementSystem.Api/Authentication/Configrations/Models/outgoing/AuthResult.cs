namespace TaskManagementSystem.Api.Authentication.Configrations.Models.outgoing;

public class AuthResult
{
    public bool Success { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public List<string> Error { get; set; }
}
