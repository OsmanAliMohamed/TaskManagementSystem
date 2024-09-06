using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.Interfaces;
using TaskManagementSystem.Models.Models;

namespace TaskManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
/*[Authorize("TeamLeader")]*/
public class TeamController(IUnitOfWork unitOfWork) : ControllerBase
{
    /*[HttpPost]*/
   /* public async Task<IActionResult> AddTeam([FromBody] AddTeamRequsetDto addTeamRequset)
    {
        await unitOfWork.Team.Add(new Team
        {
            TeamName = addTeamRequset.TeamName,
        });
    }*/
}
