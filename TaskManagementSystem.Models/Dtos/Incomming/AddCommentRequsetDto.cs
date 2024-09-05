using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Dtos.Incomming;

public class AddCommentRequsetDto
{
    public string Text { get; set; }
    public int TaskId { get; set; }
    public string UserId { get; set; }
}
