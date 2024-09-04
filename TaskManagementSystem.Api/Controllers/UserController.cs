using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Api.Authentication.Configrations;
using TaskManagementSystem.Api.Authentication.Configrations.Models.incomming;
using TaskManagementSystem.Api.Authentication.Configrations.Models.outgoing;
using TaskManagementSystem.Models.Interfaces;
using TaskManagementSystem.Models.Models;

namespace TaskManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController
    (UserManager<User> userManager,
    IUnitOfWork unitOfWork,
    IOptionsMonitor<JwtConfig> optionsMonitor) 
    : ControllerBase
{
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterationRequsetDto user)
    {
        if (ModelState.IsValid)
        {
            var userExist = await userManager.FindByEmailAsync(user.Email);
            if (userExist != null) {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Error = new List<string>()
                    {
                        "Email already in use"
                    }
                });
            }
            var newUser = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = true,
                UserName = user.Email
            };

            var isCreated = await userManager.CreateAsync(newUser,user.Password);
            if (!isCreated.Succeeded)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = isCreated.Succeeded,
                    Error = isCreated.Errors.Select(e => e.Description).ToList()
                });
            }

            var token = await GenerateJwtToken(newUser);
            return Ok(new UserRegistrationResponseDto
            {
                Success = true,
                Token = token
            });
        }
        else
        {
            return BadRequest(new UserRegistrationResponseDto()
            {
                Success = true,
                Error = new List<string>()
                {
                    "Invalid payload"
                }
            });

        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequest)
    {
        if (ModelState.IsValid)
        {
            var userExists = await userManager.FindByEmailAsync(userLoginRequest.UserName);
            if(userExists == null)
            {
                return BadRequest(new UserLoginResponseDto
                {
                    Success = false,
                    Error = new List<string>()
                    {
                        "Invalid authentication request"
                    }
                });
            }
            var isCorrect = await userManager.CheckPasswordAsync(userExists, userLoginRequest.Password);
            if (isCorrect) {
                var jwtToken = await GenerateJwtToken(userExists);
                return Ok(new UserLoginResponseDto
                {
                    Token = jwtToken,
                    Success = true,
                });
            }
            else
            {
                return BadRequest(new UserLoginResponseDto
                {
                    Success = false,
                    Error = new List<string>()
                    {
                        "Invalid authentication request"
                    }
                });
            }
        }
        else
        {
            return BadRequest(new UserLoginResponseDto()
            {
                Success = true,
                Error = new List<string>()
                {
                    "Invalid authentication requset"
                }
            });
        }
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(optionsMonitor.CurrentValue.secret);
        var jwtHandler = new JwtSecurityTokenHandler();

        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDiscriptor);
        var jwtToken = jwtHandler.WriteToken(token);
        return jwtToken;

    }
}
