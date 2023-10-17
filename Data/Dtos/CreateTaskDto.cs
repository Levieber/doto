using System.ComponentModel.DataAnnotations;

namespace doto.Data.Dtos;

public class CreateTaskDto
{
    [Required(ErrorMessage = "O título não pode estar vazio")]
    [MaxLength(150, ErrorMessage = "O título pode ter no máximo 150 caracteres")]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool Done { get; set; }
}
