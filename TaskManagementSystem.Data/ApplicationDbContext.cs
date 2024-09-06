using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.Models;
using Attachment = TaskManagementSystem.Models.Models.Attachment;
using Task = TaskManagementSystem.Models.Models.Task;


namespace TaskManagementSystem.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {}
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<TaskDependency> TaskDependencies { get; set; }
    public DbSet<UserTeam> UserTeams { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER" });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "TeamLeader", NormalizedName = "TEAMLEADER" });
        // Configure many-to-many relationship between User and Team
        /*        modelBuilder.Entity<User>()
                    .HasMany(u => u.Teams)
                    .WithMany(t => t.Members)
                    .UsingEntity(j => j.ToTable("UserTeams"));
        */


        // Configure self-referencing many-to-many relationship for Task dependencies
        modelBuilder.Entity<TaskDependency>()
            .HasKey(td => new { td.TaskId, td.DependsOnTaskId });

        modelBuilder.Entity<TaskDependency>()
            .HasOne(td => td.Task)
            .WithMany(t => t.Dependencies)
            .HasForeignKey(td => td.TaskId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskDependency>()
            .HasOne(td => td.DependsOnTask)
            .WithMany()
            .HasForeignKey(td => td.DependsOnTaskId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AuditTrail>()
            .HasOne(td => td.Task)
            .WithMany()
            .HasForeignKey(td => td.TaskId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Comment>()
            .HasOne(td => td.Task)
            .WithMany()
            .HasForeignKey(td => td.TaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

