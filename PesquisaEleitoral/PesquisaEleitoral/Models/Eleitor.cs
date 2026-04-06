using PesquisaEleitoral.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PesquisaEleitoral.Models
{
    public class Eleitor
    {
        public int EleitorId { get; set; }
        [Required]
        [StringLength(40)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(16,120)]
        public int Idade { get; set; }

        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public Regiao Regiao { get; set; }

        [JsonIgnore]
        public IntencaoDeVoto? IntencaoDeVoto { get; set; }
    }
}
