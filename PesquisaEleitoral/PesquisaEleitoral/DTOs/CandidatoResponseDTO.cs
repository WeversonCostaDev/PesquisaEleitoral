using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs
{
    public class CandidatoResponseDTO
    {   
        public int CandidatoId { get; set; } 
        public string? Nome { get; set; }
        public string? Partido { get; set; }
        public int Numero { get; set; }
    }
}
