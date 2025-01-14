using backend.DTOs;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repositorio;

        public TarefaService(ITarefaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<TarefaDTO> Todas()
        {
            return _repositorio.Todas().Select(tarefa => new TarefaDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status
            });
        }
    }
}
