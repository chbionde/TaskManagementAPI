using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models.Enums;

namespace TaskManagementAPI.DTOs;

public class UpdateTaskDto
{
    [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
    public string? Title { get; set; }

    [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(1, 4, ErrorMessage = "O status deve ser entre 1 (Pending) e 4 (Cancelled)")]
    public TaskStatusEnum Status { get; set; }

    [Range(1, 4, ErrorMessage = "A prioridade deve ser entre 1 (Low) e 4 (Critical)")]
    public PriorityEnum Priority { get; set; }

    public int? CategoryId { get; set; }
}