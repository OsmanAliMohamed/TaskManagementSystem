using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Api.Authentication.Configrations.Models.incomming;

public class RefreshTokenRequestDto
{
    [Required]
    public string Token { get; set; }
    [Required] 
    public string RefreshToken { get; set; }
}
