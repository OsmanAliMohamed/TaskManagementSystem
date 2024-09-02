using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models
{
    public class User:IdentityUser
    {
        public DateTime BirthDate  { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public ICollection<Team> Teams { get; set; }
        public ICollection<Task> TasksAssigned { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
