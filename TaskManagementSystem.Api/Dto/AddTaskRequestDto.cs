using static TaskManagementSystem.Models.Const.Enums;
using TaskStatus = TaskManagementSystem.Models.Const.Enums.TaskStatus;

namespace TaskManagementSystem.Api.Dto
{
    public class AddTaskRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        // int? AssignedToTeamId { get; set; }
        /// <summary>
        //public string CreatedByUserId { get; set; }
        /// </summary>
        public string AssignedToUserId { get; set; }

    }
}
