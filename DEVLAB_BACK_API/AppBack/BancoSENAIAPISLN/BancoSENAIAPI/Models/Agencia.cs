using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Agencia
    {
        [Key]
        public int Numeroagencia { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string siglaestado { get; set; }

    }
}
