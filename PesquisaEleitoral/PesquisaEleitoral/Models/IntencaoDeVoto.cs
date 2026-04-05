using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.Models
{
    public class IntencaoDeVoto
    {
        public int IntencaoDeVotoId { get; set; }
        [Required]
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; } = null!;
        [Required]
        public int EleitorId { get; set; }
        public Eleitor Eleitor { get; set; } = null!;
        public DateTime DataRegistro { get; set; }

    }
}
