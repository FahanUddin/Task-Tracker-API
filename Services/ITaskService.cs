using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services;

public interface ITaskService
{
    Task<IReadOnlyList<TaskItem>> GetAllAsync(string? status, string? priority);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(CreateTaskDto dto);
    Task<bool> UpdateAsync(int id, UpdateTaskDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> MarkCompletedAsync(int id);
}
