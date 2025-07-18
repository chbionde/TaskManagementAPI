using TaskManagementAPI.Models.Enums;

namespace TaskManagementAPI.DTOs;

public class ActivityDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskStatusEnum Status { get; set; }
    public PriorityEnum Priority { get; set; }
    public String? CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsOverdue => DueDate.HasValue && DueDate.Value < DateTime.Now && Status != TaskStatusEnum.COMPLETED;
}
