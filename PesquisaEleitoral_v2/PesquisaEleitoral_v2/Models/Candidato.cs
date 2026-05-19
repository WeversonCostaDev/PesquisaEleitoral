using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.Models
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
        public int Numero { get; set; }
        public ICollection<Pesquisa> PesquisaCandidatos { get; set; } = [];
    }
}
