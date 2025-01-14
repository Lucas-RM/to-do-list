using backend.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; } = default!;

        [StringLength(255)]
        public string Descricao { get; set; } = default!;

        [Required]
        public EStatusTarefa Status { get; set; } = default!;
    }
}
