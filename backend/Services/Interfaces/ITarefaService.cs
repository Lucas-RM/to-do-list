using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaDTO>> TodasAsync();
        Task<TarefaDTO> CriarAsync(TarefaDTO tarefa);
        Task<TarefaDTO> BuscarPorIdAsync(int id);
    }
}
