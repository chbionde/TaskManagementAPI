using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models.Enums;

namespace TaskManagementAPI.DTOs;

public class CreateActivityDto
{
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
    public String? Title { get; set; }

    [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(1, 4, ErrorMessage = "A prioridade deve ser entre 1 (Low) e 4 (Critical)")]
    public PriorityEnum Priority { get; set; } = PriorityEnum.MEDIUM; 

    [Required(ErrorMessage = "A categoria é obrigatória")]
    public int CategoryId { get; set; }
}
