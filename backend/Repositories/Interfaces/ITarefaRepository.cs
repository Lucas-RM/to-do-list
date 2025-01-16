using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> TodasAsync();
        Task<Tarefa> CriarAsync(Tarefa tarefa);
        Task<Tarefa> BuscarPorIdAsync(int id);
    }
}
