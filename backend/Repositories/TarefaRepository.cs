using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;

namespace backend.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DbContexto _contexto;

        public TarefaRepository(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Tarefa> Todas()
        {
            return _contexto.Tarefas.ToList();
        }
    }
}
