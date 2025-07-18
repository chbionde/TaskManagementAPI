using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementAPI.Models.Enums;

namespace TaskManagementAPI.Models;

[Table("Activities")]
public class Activity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
    [Column(TypeName = "varchar(200)")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres")]
    [Column(TypeName = "varchar(1000)")]
    public string? Description { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? DueDate { get; set; }

    [Required]
    [Column(TypeName = "int")]
    public TaskStatusEnum Status { get; set; }

    [Required]
    [Column(TypeName = "int")]
    public PriorityEnum Priority { get; set; }

    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime UpdatedAt { get; set; }

    // Navigation Properties
    public virtual Category Category { get; set; } = null!;
}
