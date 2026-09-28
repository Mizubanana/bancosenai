using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [Route("api/v1/[controller]")] // Rota ajustada para bater com o JavaScript
    [ApiController]
    public class CarteiraController : ControllerBase
    {
        private static List<Carteira> _carteiras = new List<Carteira>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carteiras);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carteira novaCarteira)
        {
            if (_carteiras.Any(c => c.Numerocarteira == novaCarteira.Numerocarteira))
            {
                return BadRequest("Já existe uma carteira cadastrada com este número.");
            }

            if (novaCarteira.Apetitecarteira < 0)
            {
                return BadRequest("O valor do apetite deve ser maior ou igual a zero.");
            }

            _carteiras.Add(novaCarteira);
            return CreatedAtAction(nameof(Get), new { id = novaCarteira.Numerocarteira }, novaCarteira);
        }

        [HttpPut("{numeroCarteira}")]
        public IActionResult Put(int numeroCarteira, [FromBody] Carteira carteiraAtualizada)
        {
            var carteiraExistente = _carteiras.FirstOrDefault(c => c.Numerocarteira == numeroCarteira);
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

            return NoContent();
        }

        [HttpDelete("{numeroCarteira}")]
        public IActionResult Delete(int numeroCarteira)
        {
            var carteira = _carteiras.FirstOrDefault(c => c.Numerocarteira == numeroCarteira);
            if (carteira == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            _carteiras.Remove(carteira);
            return NoContent();
        }
    }
}