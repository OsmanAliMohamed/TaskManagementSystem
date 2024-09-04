namespace TaskManagementSystem.Api.Authentication.Configrations;

public class JwtConfig
{
    public string secret { get; set; }
    public TimeSpan ExpireTimeFrame { get; set; }
}
