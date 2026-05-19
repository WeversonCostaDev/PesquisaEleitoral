using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.IntencoesDeVoto
{
    public class IntencaoDeVotoDTO
    {
        [Required(ErrorMessage = "Informe o Id do eleitor")]
        public int EleitorId { get; set; }

        [Required(ErrorMessage = "Informe o Id do candidato")]
        public int CandidatoId { get; set; }

        [Required (ErrorMessage ="Informe o Id da pesquisa")]
        public int PesquisaId { get; set; }

        public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
