using System.ComponentModel.DataAnnotations;
namespace PesquisaEleitoral_v2.Models
{
    public class IntencaoDeVoto
    {
        public int IntencaoDeVotoId { get; set; }
        [Required]
        public int EleitorId { get; set; }
        public Eleitor Eleitor { get; set; } = null!;
        [Required]
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; } = null!;
        [Required]
        public int PesquisaId { get; set; }
        public Pesquisa Pesquisa { get; set; } = null!;
        public DateTime DataRegistro { get; set; }
    }
}
