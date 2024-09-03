using TaskManagementSystem.Models.Models;
using Task = TaskManagementSystem.Models.Models.Task;

namespace TaskManagementSystem.Models.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Task> Task { get; }
    IBaseRepository<Comment> Comment { get; }
    IBaseRepository<Attachment> Attatchment { get; }
    IBaseRepository<AuditTrail> AuditTrail { get; }
    IBaseRepository <TaskDependency> TaskDependency { get; }
    IBaseRepository<Team> Team { get; }
    IBaseRepository<User> User { get; }

    void CompleteAsync(); 

}
