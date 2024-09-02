using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class TaskDependency
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int DependsOnTaskId { get; set; }
        public Task DependsOnTask { get; set; }
    }
}
