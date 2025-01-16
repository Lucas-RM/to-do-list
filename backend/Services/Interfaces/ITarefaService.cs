using backend.DTOs;

namespace backend.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaDTO>> TodasAsync();
        Task<TarefaDTO> CriarAsync(TarefaDTO tarefa);
    }
}
