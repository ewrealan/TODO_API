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

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var newTask = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                PriorityLevel = dto.PriorityLevel,
                DueDate = dto.DueDate,
                CreatedDate = DateTime.Now,
                IsCompleted = false
            };

            var createdTask = await _taskService.CreateAsync(newTask);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // ✅ PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var updatedTask = new TaskItem
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                PriorityLevel = dto.PriorityLevel,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted
            };

            var result = await _taskService.UpdateAsync(id, updatedTask);
            if (!result) return NotFound();

            return Ok(updatedTask); // ✅ Angular güncellenen nesneyi bekliyor
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // PATCH: api/tasks/{id}/complete
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var result = await _taskService.ToggleCompleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
