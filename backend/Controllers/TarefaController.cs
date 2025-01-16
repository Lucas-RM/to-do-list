using backend.DTOs;
using backend.Enums;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
        public async Task<IActionResult> Todas()
        {
            return Ok(await _servico.TodasAsync());
        }

        // Método para criar uma nova tarefa
        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] TarefaDTO tarefaDto)
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
                return Ok(new { mensagem = "Tarefa criada com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar a tarefa.", erro = ex.Message });
            }
        }
    }
}
