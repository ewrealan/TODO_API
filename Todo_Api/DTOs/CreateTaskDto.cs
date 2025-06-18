namespace Todo_Api.Dtos
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int PriorityLevel { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
