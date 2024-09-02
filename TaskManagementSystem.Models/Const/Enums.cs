using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Const
{
    public class Enums
    {
        public enum UserRole
        {
            RegularUser,
            TeamLead,
            Administrator
        }

        public enum TaskPriority
        {
            Low,
            Medium,
            High
        }

        public enum TaskStatus
        {
            ToDo,
            InProgress,
            Completed
        }

    }
}
