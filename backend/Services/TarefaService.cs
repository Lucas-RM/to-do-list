using backend.DTOs;
using backend.Exceptions;
using backend.Models;
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

        public async Task<IEnumerable<TarefaDTO>> TodasAsync()
        {
            var tarefas = await _repositorio.TodasAsync();

            return tarefas.Select(tarefa => new TarefaDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status
            });
        }

        public async Task<TarefaDTO> CriarAsync(TarefaDTO tarefaDto)
        {
            var tarefa = new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                Status = tarefaDto.Status
            };

            var novaTarefa = await _repositorio.CriarAsync(tarefa);

            var novaTarefaDTO = new TarefaDTO
            {
                Id = novaTarefa.Id,
                Descricao = novaTarefa.Descricao,
                Status = novaTarefa.Status
            };

            return novaTarefaDTO;
        }

        public async Task<TarefaDTO> BuscarPorIdAsync(int id)
        {
            var tarefa = await _repositorio.BuscarPorIdAsync(id);

            if (tarefa == null)
            {
                throw new NotFoundException("Tarefa não encontrada.");
            }

            var tarefaDTO = new TarefaDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status
            };

            return tarefaDTO;
        }
    }
}
