using Microsoft.EntityFrameworkCore;
using Todo_Api.Data;
using Todo_Api.Dtos;
using Todo_Api.Models;

namespace Todo_Api.Services
{
    public class TaskService
    {
        private readonly TodoContext _context;

        public TaskService(TodoContext context)
        {
            _context = context;
        }

        // ✅ Görev listesini DTO ile getir
        public async Task<List<TaskDto>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Category)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CategoryName = t.Category!.Name,
                    CategoryColor = t.Category.Color,
                    DueDate = t.DueDate,
                    PriorityLevel = t.PriorityLevel,
                    IsCompleted = t.IsCompleted
                })
                .ToListAsync();
        }

        // ✅ Görev ID ile getir (DTO)
        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.Id == id)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CategoryName = t.Category!.Name,
                    CategoryColor = t.Category.Color,
                    DueDate = t.DueDate,
                    PriorityLevel = t.PriorityLevel,
                    IsCompleted = t.IsCompleted
                })
                .FirstOrDefaultAsync();
        }

        // ✅ Yeni görev oluştur
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // ✅ Görev güncelle
        public async Task<bool> UpdateAsync(int id, TaskItem updatedTask)
        {
            var existing = await _context.Tasks.FindAsync(id);
            if (existing == null) return false;

            existing.Title = updatedTask.Title;
            existing.Description = updatedTask.Description;
            existing.CategoryId = updatedTask.CategoryId;
            existing.PriorityLevel = updatedTask.PriorityLevel;
            existing.DueDate = updatedTask.DueDate;
            existing.IsCompleted = updatedTask.IsCompleted;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Görev sil
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Görev tamamlandı/geri al toggle
        public async Task<bool> ToggleCompleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}