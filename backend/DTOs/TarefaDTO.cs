using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; } = default!;

        [Required(ErrorMessage = "O campo 'Titulo' é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O 'Titulo' não pode ter mais que 50 caracteres.")]
        public string Titulo { get; set; } = default!;

        [Required(ErrorMessage = "O campo 'Descricao' é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O 'Descricao' não pode ter mais que 255 caracteres.")]
        public string Descricao { get; set; } = default!;

        [Required(ErrorMessage = "O campo 'Status' é obrigatório.")]
        [EnumDataType(typeof(EStatusTarefa), ErrorMessage = "O campo 'Status' deve ser 0 [Pendente], 1 [Em Andamento] ou 2 [Concluida].")]
        public EStatusTarefa Status { get; set; } = default!;
    }
}
