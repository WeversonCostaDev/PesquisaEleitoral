using PesquisaEleitoral.Models;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs.IntencaoDeVotos
{
    public class IntencaoDeVotosResponseDTO
    {
        public int IntencaoDeVotoId { get; set; }
        public Candidato? Candidato { get; set; }
        public Eleitor? Eleitor { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
