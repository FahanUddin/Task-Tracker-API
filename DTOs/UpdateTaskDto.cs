using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.DTOs;

public class UpdateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    [RegularExpression("Low|Medium|High")]
    public string Priority { get; set; } = "Medium";

    public DateTime? DueDate { get; set; }
}
