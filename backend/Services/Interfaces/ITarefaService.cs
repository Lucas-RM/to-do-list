using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface ITarefaService
    {
        IEnumerable<TarefaDTO> Todas();
    }
}
