using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Pesquisas
{
    public class PesquisaCandidatoDTO
    {
        [Required (ErrorMessage ="Informe o Id da pesquisa.")]
        public int PesquisaId { get; set; }

        [Required(ErrorMessage = "Informe o Id do candidato.")]
        public int CandidatoId{ get; set; }
    }
}
