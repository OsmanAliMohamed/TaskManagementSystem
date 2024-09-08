using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Api.Dto;
using TaskManagementSystem.Models.Dtos;
using TaskManagementSystem.Models.Dtos.Incomming;
using TaskManagementSystem.Models.Interfaces;

namespace TaskManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
/*[Authorize(Roles = "User")]*/
public class TaskController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await unitOfWork.Task.FindAsync(x => x.TaskId == id, x => x.Comments, x => x.Attachments);
        return Ok(result.FirstOrDefault());
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await unitOfWork.Task.GetAllAsync();
        return Ok(tasks);
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        await unitOfWork.Task.DeleteAsync(id);
        unitOfWork.CompleteAsync();
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] Api.Dto.AddTaskRequestDto task)
    {
        var ans = await unitOfWork.Task.FindAsync(t => t.TaskId == id);
        var matched = ans.FirstOrDefault();
        if (matched != null)
        {
            matched.Title = task.Title;
            matched.Priority = task.Priority;
            matched.Status = task.Status;
            matched.Description = task.Description; 
            matched.DueDate = task.DueDate;
        }
        unitOfWork.CompleteAsync();
        return Ok();

    }
    
}
