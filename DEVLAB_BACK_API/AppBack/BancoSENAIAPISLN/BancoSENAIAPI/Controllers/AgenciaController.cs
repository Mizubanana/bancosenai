using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AgenciaController : ControllerBase
    {
        private static List<Agencia> _agencias = new List<Agencia>
        {
            new Agencia { Numeroagencia = 1001, Cidade = "Aracaju", siglaestado = "SE" },
            new Agencia { Numeroagencia = 2002, Cidade = "São Paulo", siglaestado = "SP" },
            new Agencia { Numeroagencia = 3003, Cidade = "Salvador", siglaestado = "BA" }
        };

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(_agencias);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Agencia novaAgencia)
        {
            
            if (_agencias.Any(a => a.Numeroagencia == novaAgencia.Numeroagencia))
                return BadRequest(new { message = "Este número de agência já existe." });

            _agencias.Add(novaAgencia);
            // Retorna Status 201 Created conforme boas práticas REST [6, 8]
            return Created("", novaAgencia);
        }

        [HttpGet("{codigo}")]
        public IActionResult ConsultarPorCodigo(int codigo)
        {
            var agencia = _agencias.FirstOrDefault(a => a.Numeroagencia == codigo);

            if (agencia == null)
                return NotFound(new { message = "Agência não encontrada." }); // Status 404 [6, 7]

            return Ok(agencia); // Status 200 OK [6, 7]
        }

        [HttpPut("{codigo}")]
        public IActionResult Alterar(int codigo, [FromBody] Agencia agenciaAtualizada)
        {
            var agenciaExistente = _agencias.FirstOrDefault(a => a.Numeroagencia == codigo);

            if (agenciaExistente == null) return NotFound();

            agenciaExistente.Cidade = agenciaAtualizada.Cidade;
            agenciaExistente.siglaestado = agenciaAtualizada.siglaestado;

            // Retorna Status 204 No Content para atualizações bem-sucedidas [6, 9]
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public IActionResult Excluir(int codigo)
        {
            var agencia = _agencias.FirstOrDefault(a => a.Numeroagencia == codigo);

            if (agencia == null) return NotFound();

            _agencias.Remove(agencia);
            return Ok(new { message = "Agência excluída com sucesso." }); // Status 200 [6]
        }
    }
}
