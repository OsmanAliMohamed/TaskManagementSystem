using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Api.Authentication.Configrations;
using TaskManagementSystem.Api.Authentication.Configrations.Models.Generic;
using TaskManagementSystem.Api.Authentication.Configrations.Models.incomming;
using TaskManagementSystem.Api.Authentication.Configrations.Models.outgoing;
using TaskManagementSystem.Models.Dtos.Incomming;
using TaskManagementSystem.Models.Interfaces;
using TaskManagementSystem.Models.Models;

namespace TaskManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController
    (UserManager<User> userManager,
    IUnitOfWork unitOfWork,
    TokenValidationParameters tokenValidationParameters,
    IOptionsMonitor<JwtConfig> optionsMonitor) 
    : ControllerBase
{
    [HttpPost("Register")]
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
            var result = await userManager.AddToRoleAsync(newUser, "User");
            var token = await GenerateJwtTokenAsync(newUser);
            return Ok(new UserRegistrationResponseDto
            {
                Success = true,
                Token = token.Token,
                RefreshToken = token.RefreshToken
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

    [HttpPost("Login")]
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
                var jwtToken = await GenerateJwtTokenAsync(userExists);
                return Ok(new UserLoginResponseDto
                {
                    Token = jwtToken.Token,
                    RefreshToken = jwtToken.RefreshToken,
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

    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRoleToUser(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found"); //change it
        }

        var result = await userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors); //change it
        }

        return Ok("Role added successfully"); //change it
    }


    [HttpGet("Users")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await unitOfWork.User.GetAllAsync());
    }
    [HttpGet("Id")]
    public async Task<IActionResult> GetById(string Id)
    {
        var user = await unitOfWork.User.FindAsync(user => user.Id == Id);
        if (user != null)
        {
            var res = await unitOfWork.UserTeam.FindAsync(x => x.Id == Id);
            user.FirstOrDefault().Teams = res.ToList();
        }
        return Ok(user);
    }

    /*[Authorize(Roles = "TeamLeader")]*/
    [HttpPost("AssignTeam")]
    public async Task<IActionResult> AssignUserToTeam([FromBody] AssignUserToTeamRequestDto assignUserToTeamRequest)
    {
        var isExist = await userManager.FindByIdAsync(assignUserToTeamRequest.UserId);
        var isTeamExist = await unitOfWork.Team.GetByIdAsync(assignUserToTeamRequest.TeamId);
        if (isExist != null && isTeamExist != null)
        {
            await unitOfWork.UserTeam.AddAsync(new UserTeam { Id = assignUserToTeamRequest.UserId, TeamId = assignUserToTeamRequest.TeamId });
            unitOfWork.CompleteAsync();
        }
        else
        {
            return BadRequest("Invalid payload");
        }

        return Ok($"This user {isExist.Id} is assigned to this team {isTeamExist.TeamId}");
    }

    /*[HttpPost]
    [Route("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto tokenData)
    {
        if (ModelState.IsValid)
        {
            var result = VerifyToken(tokenData);
        }
        else
        {
            return BadRequest(new UserLoginResponseDto
            {
                Success = false,
                Error = new List<string>()
                    {
                        "Invalid payload"
                    }
            });
        }
    }*/
    private async Task<TokenData> GenerateJwtTokenAsync(User user)
    {
        var key = Encoding.ASCII.GetBytes(optionsMonitor.CurrentValue.secret);
        var jwtHandler = new JwtSecurityTokenHandler();
        
        var roles = await userManager.GetRolesAsync(user);
        var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray();
        
        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            }.Concat(claims)),
            Expires = DateTime.UtcNow.Add(optionsMonitor.CurrentValue.ExpireTimeFrame),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDiscriptor);
        var jwtToken = jwtHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            AddedDate = DateTime.UtcNow,
            Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
            UserId = user.Id,
            IsRevoked = false,
            IsUsed = false,
            JwtId = token.Id,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await unitOfWork.RefreshToken.AddAsync(refreshToken);
        unitOfWork.CompleteAsync();

        return new TokenData
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };

    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
