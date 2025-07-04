﻿namespace Todo_Api.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryColor { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityLevel { get; set; }
        public bool IsCompleted { get; set; }
    }
}
