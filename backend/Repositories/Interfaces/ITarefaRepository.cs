using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        IEnumerable<Tarefa> Todas();
    }
}
