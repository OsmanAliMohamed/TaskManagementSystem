using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Models.Models;
[PrimaryKey(nameof(Id), nameof(TeamId))]
public class UserTeam
{
    public string Id { get; set; }
    public int TeamId { get; set; }

}
