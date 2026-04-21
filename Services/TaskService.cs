using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(string? status, string? priority)
    {
        IQueryable<TaskItem> query = _context.Tasks.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(status))
        {
            var isCompleted = status.Equals("completed", StringComparison.OrdinalIgnoreCase);
            var isPending = status.Equals("pending", StringComparison.OrdinalIgnoreCase);

            if (isCompleted)
            {
                query = query.Where(t => t.IsCompleted);
            }
            else if (isPending)
            {
                query = query.Where(t => !t.IsCompleted);
            }
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            query = query.Where(t => t.Priority.ToLower() == priority.ToLower());
        }

        return await query
            .OrderBy(t => t.IsCompleted)
            .ThenBy(t => t.DueDate)
            .ThenByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            DueDate = dto.DueDate
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsCompleted = dto.IsCompleted;
        task.Priority = dto.Priority;
        task.DueDate = dto.DueDate;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkCompletedAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
