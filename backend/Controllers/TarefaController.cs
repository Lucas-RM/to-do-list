using backend.DTOs;
using backend.Exceptions;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("tarefas")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _servico;

        public TarefaController(ITarefaService servico)
        {
            _servico = servico;
        }

        // Método para listar todas as tarefas
        [HttpGet]
        public async Task<ActionResult<TarefaDTO>> Todas()
        {
            return Ok(await _servico.TodasAsync());
        }

        // Método para criar uma nova tarefa
        [HttpPost("criar")]
        public async Task<ActionResult<TarefaDTO>> Criar([FromBody] TarefaDTO tarefaDto)
        {
            if (!ModelState.IsValid)
            {
                // Retorna os erros de validação encontrados no objeto de entrada (TarefaDTO)
                var erros = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { Mensagem = "Erro na validação dos dados.", Erros = erros });
            }

            try
            {
                await _servico.CriarAsync(tarefaDto);
                return StatusCode(201, new { mensagem = "Tarefa criada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar a tarefa.", erro = ex.Message });
            }
        }

        // Método para buscar uma tarefa por id
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaDTO>> BuscarPorId(int id)
        {
            try
            {
                var tarefa = await _servico.BuscarPorIdAsync(id);
                return Ok(tarefa);
            }
            catch (NotFoundException ex)
            {
                // Retorna 404 para tarefas não encontradas
                return NotFound(new { mensagem = ex.Message });
            }
            catch (BadRequestException ex)
            {
                // Retorna 400 para erros de requisição inválida
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                // Retorna 500 para erros internos
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", erro = ex.Message });
            }
        }
    }
}
