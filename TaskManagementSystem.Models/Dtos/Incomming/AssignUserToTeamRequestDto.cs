using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Dtos.Incomming;

public class AssignUserToTeamRequestDto
{
    public string UserId { get; set; }
    public int TeamId { get; set; }
}
