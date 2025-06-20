using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string IconName { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
}
