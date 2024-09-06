using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.Dtos.Incomming;
using TaskManagementSystem.Models.Interfaces;

namespace TaskManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
/*[Authorize(Roles = "User")]*/
public class TaskController (IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await unitOfWork.Task.FindAsync(x => x.AssignedToUserId == id, x => x.Comments, x => x.Attachments);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await unitOfWork.Task.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskManagementSystem.Api.Dto.AddTaskRequestDto addTaskRequest)
    {
        await unitOfWork.Task.AddAsync(new Models.Models.Task
        {
            //AssignedToTeamId = addTaskRequest.AssignedToTeamId,
            AssignedToUserId = addTaskRequest.AssignedToUserId,////addTaskRequest.AssignedToUserId,
            CreatedAt = DateTime.UtcNow,
            DueDate = addTaskRequest.DueDate,
            Description = addTaskRequest.Description,
            Title = addTaskRequest.Title,
            Priority = addTaskRequest.Priority,
            Status = addTaskRequest.Status,
            CreatedByUserId = addTaskRequest.AssignedToUserId
        });
        unitOfWork.CompleteAsync();
        return Ok();
    }
    [HttpPost("Comment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentRequsetDto addCommentRequset)
    {
        await unitOfWork.Comment.AddAsync(new Models.Models.Comment
        {
            CreatedAt = DateTime.UtcNow,
            TaskId = addCommentRequset.TaskId,
            UserId = addCommentRequset.UserId,
            Text = addCommentRequset.Text
        });
        unitOfWork.CompleteAsync();
        return Ok();
    }

    [HttpGet("Comment")]
    public async Task<IActionResult> GetAllComment()
    {
        return Ok(await unitOfWork.Comment.GetAllAsync());
    }
}
