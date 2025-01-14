using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _servico;

        public TarefaController(ITarefaService servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public IActionResult Todas()
        {
            return Ok(_servico.Todas());
        }
    }
}
