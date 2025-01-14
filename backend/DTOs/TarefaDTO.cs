using backend.Enums;

namespace backend.DTOs
{
    public class TarefaDTO
    {
        public int Id { get; set; } = default!;
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public EStatusTarefa Status { get; set; } = default!;
    }
}
