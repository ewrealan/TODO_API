namespace Todo_Api.Dtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityLevel { get; set; }

        public string CategoryName { get; set; } = null!; // ❗ categoryId yok!

        public bool IsCompleted { get; set; }
    }
}
