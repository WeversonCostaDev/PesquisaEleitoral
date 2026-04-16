using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PesquisaEleitoral.Models
{
    public class Candidato
    {
        public int CandidatoId { get; set; }
        [Required]
        [StringLength(40)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string Partido { get; set; } = string.Empty;

        [Required]
        [Range(10,99)]
        public int Numero { get; set; }

        [JsonIgnore]
        public IEnumerable<IntencaoDeVoto>? IntencaoDeVoto { get; set; }
    }
}
