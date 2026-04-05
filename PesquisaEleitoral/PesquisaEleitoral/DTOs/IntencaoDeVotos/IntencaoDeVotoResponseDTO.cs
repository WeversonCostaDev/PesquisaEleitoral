using PesquisaEleitoral.DTOs.Candidatos;
using PesquisaEleitoral.DTOs.Eleitores;

namespace PesquisaEleitoral.DTOs.IntencaoDeVotos
{
    public class IntencaoDeVotoResponseDTO
    {
        public int IntencaoDeVotoId { get; set; }
        public CandidatoResponseDTO Candidato { get; set; } = null!;
        public EleitorResponseDTO Eleitor { get; set; } = null!;
        public DateTime DataRegistro { get; set; }
    }
}
