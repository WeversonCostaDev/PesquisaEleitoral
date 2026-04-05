using PesquisaEleitoral.Models;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs.IntencaoDeVotos
{
    public class IntencaoDeVotoDTO
    {
        [Required]
        public int CandidatoId { get; set; }
        [Required]
        public int EleitorId { get; set; }
        public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}
