using Microsoft.AspNetCore.Mvc;
using Todo_Api.Dtos;
using Todo_Api.Models;
using Todo_Api.Services;

namespace Todo_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly CategoryService _categoryService;

        public TasksController(TaskService taskService, CategoryService categoryService)
        {
            _taskService = taskService;
            _categoryService = categoryService;
        }

        // ✅ Tüm görevleri getir
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        // ✅ Belirli bir görevi getir
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // ✅ Yeni görev oluştur (categoryName ile)
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            // categoryName → categoryId çevir
            var category = await _categoryService.GetByNameAsync(dto.CategoryName);
            if (category == null)
                return BadRequest("Geçersiz kategori adı.");

            var newTask = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = category.Id, // Elle değil, sistem belirler
                PriorityLevel = dto.PriorityLevel,
                DueDate = dto.DueDate,
                CreatedDate = DateTime.Now,
                IsCompleted = dto.IsCompleted
            };

            var createdTask = await _taskService.CreateAsync(newTask);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // ✅ Görev güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var category = await _categoryService.GetByNameAsync(dto.CategoryName);
            if (category == null)
                return BadRequest("Geçersiz kategori adı.");

            var updatedTask = new TaskItem
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = category.Id,
                PriorityLevel = dto.PriorityLevel,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted
            };

            var result = await _taskService.UpdateAsync(id, updatedTask);
            if (!result) return NotFound();

            return Ok(updatedTask);
        }

        // ✅ Görev sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // ✅ Görev tamamlama toggle
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var result = await _taskService.ToggleCompleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
