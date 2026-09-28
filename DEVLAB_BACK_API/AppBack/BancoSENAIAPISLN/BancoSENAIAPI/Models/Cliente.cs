using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; } = string.Empty;

        public int numeroagencia { get; set; } = 10;

        public decimal Saldo { get; set; } = 0.0m;

        public DateTime? DataNascimento { get; set; }
        public string? Sexo { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
    }
}