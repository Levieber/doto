using System.ComponentModel.DataAnnotations;

namespace doto.Data.Dtos;

public class UpdateTaskDto
{
    [MaxLength(150, ErrorMessage = "O título pode ter no máximo 150 caracteres")]
    [Required]
    public string Title { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public bool Done { get; set; }
}
