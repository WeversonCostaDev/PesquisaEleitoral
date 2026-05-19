using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Candidatos
{
    public class CandidatoResponseDTO
    {
        public int CandidatoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Partido { get; set; } = string.Empty;
        public int Numero { get; set; }
    }
}
