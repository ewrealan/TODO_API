namespace Todo_Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#000000";
        public string IconName { get; set; } = "default";
        public bool IsDefault { get; set; }

        // Navigation property
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
