using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public ICollection<User> Members { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
