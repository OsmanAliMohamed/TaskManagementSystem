using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.Dtos.Incomming;
using TaskManagementSystem.Models.Interfaces;
using TaskManagmentSystem.Business;

namespace TaskManagementSystem.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttatchmentController (IUnitOfWork unitOfWork):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAttachment([FromBody] AddAttachmentRequestDto addAttachmentRequest)
    {
        await unitOfWork.Attatchment.AddAsync(new Models.Models.Attachment
        {
            FileName = addAttachmentRequest.FileName,
            FilePath = addAttachmentRequest.FilePath,
            TaskId = addAttachmentRequest.TaskId
        });
        unitOfWork.CompleteAsync();
        return Ok();
    }
    [HttpGet("Attachment")]
    public async Task<IActionResult> GetAttachmentById(int id)
    {
        return Ok(await unitOfWork.Attatchment.GetByIdAsync(id));
    }

}
