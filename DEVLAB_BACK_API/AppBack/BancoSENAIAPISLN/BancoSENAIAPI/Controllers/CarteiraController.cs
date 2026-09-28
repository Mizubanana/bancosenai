using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [Route("api/v1/[controller]")] 
    [ApiController]
    public class CarteiraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarteiraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carteiras = await _context.Carteira.ToListAsync();
            return Ok(carteiras);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Carteira novaCarteira)
        {
            if (await _context.Carteira.AnyAsync(c => c.Numerocarteira == novaCarteira.Numerocarteira))
            {
                return BadRequest("Já existe uma carteira cadastrada com este número.");
            }

            if (novaCarteira.Apetitecarteira < 0)
            {
                return BadRequest("O valor do apetite deve ser maior ou igual a zero.");
            }

            await _context.Carteira.AddAsync(novaCarteira);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = novaCarteira.Numerocarteira }, novaCarteira);
        }

        [HttpPut("{numeroCarteira}")]
        public async Task<IActionResult> Put(int numeroCarteira, [FromBody] Carteira carteiraAtualizada)
        {
            var carteiraExistente = await _context.Carteira.FirstOrDefaultAsync(c => c.Numerocarteira == numeroCarteira);
            if (carteiraExistente == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            if (carteiraAtualizada.Apetitecarteira < 0)
            {
                return BadRequest("O valor do apetite deve ser maior ou igual a zero.");
            }

            carteiraExistente.Nomecarteira = carteiraAtualizada.Nomecarteira;
            carteiraExistente.Apetitecarteira = carteiraAtualizada.Apetitecarteira;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{numeroCarteira}")]
        public async Task<IActionResult> Delete(int numeroCarteira)
        {
            var carteira = await _context.Carteira.FirstOrDefaultAsync(c => c.Numerocarteira == numeroCarteira);
            if (carteira == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            _context.Carteira.Remove(carteira);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}