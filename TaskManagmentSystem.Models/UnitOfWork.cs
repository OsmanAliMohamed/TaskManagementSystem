using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Interfaces;
using TaskManagementSystem.Models.Models;
using TaskManagmentSystem.Business.Repositories;
using Task = TaskManagementSystem.Models.Models.Task;

namespace TaskManagmentSystem.Business;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    public IBaseRepository<Task> Task { get; private set; }

    public IBaseRepository<Comment> Comment { get; private set; }

    public IBaseRepository<Attachment> Attatchment { get; private set; }

    public IBaseRepository<AuditTrail> AuditTrail { get; private set; }

    public IBaseRepository<TaskDependency> TaskDependency { get; private set; }

    public IBaseRepository<Team> Team { get; private set; }
    public IBaseRepository<UserTeam> UserTeam { get; private set; }

    public IUserRepository User { get; private set; }

    public IBaseRepository<RefreshToken> RefreshToken { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Task = new BaseRepository<Task>(_context);
        Comment = new BaseRepository<Comment>(_context);
        Attatchment = new BaseRepository<Attachment>(_context);
        AuditTrail = new BaseRepository<AuditTrail>(_context);
        TaskDependency = new BaseRepository<TaskDependency>(_context);
        Team = new BaseRepository<Team>(_context);
        UserTeam = new BaseRepository<UserTeam>(_context);
        User = new UserRepository(_context);
        RefreshToken = new BaseRepository<RefreshToken>(_context);
    }

    public void CompleteAsync()
    {
        _context.SaveChanges();
    }
    public void Dispose()
    {
        _context.DisposeAsync();
    }
}
