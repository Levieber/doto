using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doto.Models;

public class TaskModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O título não pode estar vazio")]
    [MaxLength(150, ErrorMessage = "O título pode ter no máximo 150 caracteres")]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool Done { get; set; }
}
