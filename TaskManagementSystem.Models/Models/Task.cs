using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManagementSystem.Models.Const.Enums;
using TaskStatus = TaskManagementSystem.Models.Const.Enums.TaskStatus;

namespace TaskManagementSystem.Models.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public string AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }

        public int? AssignedToTeamId { get; set; }
        public Team AssignedToTeam { get; set; }

        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<TaskDependency> Dependencies { get; set; }
        public ICollection<AuditTrail> AuditTrail { get; set; }
        public Task()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
