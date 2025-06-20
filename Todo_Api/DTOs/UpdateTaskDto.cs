namespace Todo_Api.Dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string CategoryName { get; set; } = null!; // ❗ categoryId yok!

        public int PriorityLevel { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
