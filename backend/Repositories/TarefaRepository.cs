using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DbContexto _contexto;

        public TarefaRepository(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Tarefa>> TodasAsync()
        {
            return await _contexto.Tarefas.ToListAsync();
        }

        public async Task<Tarefa> CriarAsync(Tarefa tarefa)
        {
            await _contexto.Tarefas.AddAsync(tarefa);
            await _contexto.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> BuscarPorIdAsync(int id)
        {
            return await _contexto.Tarefas.FindAsync(id);
        }
    }
}
