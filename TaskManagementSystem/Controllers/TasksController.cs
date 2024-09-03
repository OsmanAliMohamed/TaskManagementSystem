using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Dtos;
using TaskManagementSystem.Models.Models;
using static TaskManagementSystem.Models.Const.Enums;
using Task = TaskManagementSystem.Models.Models.Task;
using TaskStatus = TaskManagementSystem.Models.Const.Enums.TaskStatus;


namespace TaskManagementSystem.Controllers;

public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Tasks
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Tasks.Include(t => t.AssignedToTeam).Include(t => t.AssignedToUser);
        return View(await applicationDbContext.ToListAsync());
    }
    public void TaskFields()
    {
        ViewData["AssignedToTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName");
        ViewData["AssignedToUserId"] = new SelectList(_context.Users, "Id", "Id");
        List<string> priority = Enum.GetNames(typeof(TaskPriority)).ToList();
        List<SelectListItem> selectList = priority.Select(option => new SelectListItem { Value = option, Text = option }).ToList();
        ViewData["priority"] = selectList;
        List<string> status = Enum.GetNames(typeof(TaskStatus)).ToList();
        List<SelectListItem> statuslist = status.Select(option => new SelectListItem { Value = option, Text = option }).ToList();
        ViewData["status"] = statuslist;
    }
    public Task MappingTask(TaskDto task)
    {
        Task _task = new Task();
        _task.TaskId = task.TaskId;
        _task.Status = task.Status;
        _task.Title = task.Title;
        _task.Description = task.Description;
        _task.DueDate = task.DueDate;
        _task.Priority = task.Priority;
        _task.AssignedToTeamId = task.AssignedToTeamId;
        _task.AssignedToUserId = task.AssignedToUserId;
        _task.CreatedAt = DateTime.Now;
        _task.CreatedByUserId = task.CreatedByUserId;
        return _task;
    }
    // GET: Tasks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .Include(t => t.AssignedToTeam)
            .Include(t => t.AssignedToUser)
            .FirstOrDefaultAsync(m => m.TaskId == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // GET: Tasks/Create
    public IActionResult Create()
    {
        TaskFields();
        return View();
    }

    // POST: Tasks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskDto task)
    {
        if (ModelState.IsValid)
        {
            Task _task = MappingTask(task);
            _context.Add(_task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AssignedToTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", task.AssignedToTeamId);
        ViewData["AssignedToUserId"] = new SelectList(_context.Users, "Id", "Id", task.AssignedToUserId);
        return View(task);
    }

    // GET: Tasks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        TaskFields();
        return View(task);
    }

    // POST: Tasks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,TaskDto task)
    {
        if (id != task.TaskId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                Task _task = MappingTask(task);
                _context.Add(_task);
                _context.Update(_task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(task.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["AssignedToTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName", task.AssignedToTeamId);
        ViewData["AssignedToUserId"] = new SelectList(_context.Users, "Id", "Id", task.AssignedToUserId);
        return View(task);
    }

    // GET: Tasks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .Include(t => t.AssignedToTeam)
            .Include(t => t.AssignedToUser)
            .FirstOrDefaultAsync(m => m.TaskId == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Tasks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.TaskId == id);
    }
}
