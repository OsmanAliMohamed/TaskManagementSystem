using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.Dtos.Incomming;
using TaskManagementSystem.Models.Interfaces;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Authorize(Roles = "User")]
public class TaskController (IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await unitOfWork.Task.GetByIdAsync(id));
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await unitOfWork.Task.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskRequestDto addTaskRequest)
    {
        await unitOfWork.Task.AddAsync(new Models.Models.Task
        {
            AssignedToTeamId = addTaskRequest.AssignedToTeamId,
            AssignedToUserId = addTaskRequest.AssignedToUserId,
            CreatedAt = DateTime.UtcNow,
            DueDate = addTaskRequest.DueDate,
            Description = addTaskRequest.Description,
            Title = addTaskRequest.Title,
            Priority = addTaskRequest.Priority,
            Status = addTaskRequest.Status,
            CreatedByUserId = addTaskRequest.CreatedByUserId
        });
        unitOfWork.CompleteAsync();
        return Ok();
    }

}
