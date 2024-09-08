using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Api.Authentication.Configrations.Models.incomming;

public class RegisterationRequsetDto
{
    [Required]
    public string FirstName { get; set; }
    [Required] 
    public string LastName { get; set; }
    [Required] 
    public string Email { get; set; }
    [Required] 
    public string Password { get; set; }

}
