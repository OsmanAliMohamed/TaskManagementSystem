using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.Models.Const.Enums;
using TaskStatus = TaskManagementSystem.Models.Const.Enums.TaskStatus;

namespace TaskManagementSystem.Models.Dtos.Incomming;

public class AddTaskRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public int? AssignedToTeamId { get; set; }
    public string CreatedByUserId { get; set; }
    public string AssignedToUserId { get; set; }

}
