namespace TaskManagementSystem.Models.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public ICollection<UserTeam> Members { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
