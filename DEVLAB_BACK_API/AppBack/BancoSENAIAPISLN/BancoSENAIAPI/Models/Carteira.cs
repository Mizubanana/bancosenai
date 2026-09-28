using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Carteira
    {
        [Key]
        [Required(ErrorMessage = "O número da carteira é obrigatório.")]
        public int Numerocarteira { get; set; }

        [Required(ErrorMessage = "O nome da carteira é obrigatório.")]
        public string Nomecarteira { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "O apetite da carteira deve ser maior ou igual a zero.")]
        public decimal Apetitecarteira { get; set; } = 1000000.00m;
    }
}