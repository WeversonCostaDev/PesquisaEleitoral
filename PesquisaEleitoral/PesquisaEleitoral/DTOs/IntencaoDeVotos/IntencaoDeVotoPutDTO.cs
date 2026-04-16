using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs.IntencaoDeVotos
{
    public class IntencaoDeVotoPutDTO
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public int IntencaoDeVotoId { get; set; }

        [Required(ErrorMessage = "O Id do candidato é obrigatório.")]
        public int CandidatoId { get; set; }

        [Required(ErrorMessage = "O Id do Eleitor é obrigatório.")]
        public int EleitorId { get; set; }
        public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
