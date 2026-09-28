using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente novoCliente)
        {
            if (string.IsNullOrWhiteSpace(novoCliente.Nome) || string.IsNullOrWhiteSpace(novoCliente.CPF))
            {
                return BadRequest("Nome e CPF são obrigatórios.");
            }

            if (novoCliente.numeroagencia == 0) novoCliente.numeroagencia = 10;

            await _context.Cliente.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = novoCliente.Codigo }, novoCliente);
        }

        [HttpPut("{codigoCliente}")]
        public async Task<IActionResult> Put(int codigoCliente, [FromBody] Cliente clienteAtualizado)
        {
            var clienteExistente = await _context.Cliente.FirstOrDefaultAsync(c => c.Codigo == codigoCliente);
            if (clienteExistente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            if (string.IsNullOrWhiteSpace(clienteAtualizado.Nome) || string.IsNullOrWhiteSpace(clienteAtualizado.CPF))
            {
                return BadRequest("Nome e CPF são obrigatórios.");
            }

            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.CPF = clienteAtualizado.CPF;
            clienteExistente.DataNascimento = clienteAtualizado.DataNascimento;
            clienteExistente.Sexo = clienteAtualizado.Sexo;
            clienteExistente.Endereco = clienteAtualizado.Endereco;
            clienteExistente.Cidade = clienteAtualizado.Cidade;
            clienteExistente.Estado = clienteAtualizado.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{codigoCliente}")]
        public async Task<IActionResult> Delete(int codigoCliente)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Codigo == codigoCliente);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}