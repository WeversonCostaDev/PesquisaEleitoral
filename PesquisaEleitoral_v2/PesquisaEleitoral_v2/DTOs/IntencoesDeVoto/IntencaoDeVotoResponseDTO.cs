using PesquisaEleitoral_v2.DTOs.Candidatos;
using PesquisaEleitoral_v2.DTOs.Eleitores;
using PesquisaEleitoral_v2.DTOs.Pesquisas;

namespace PesquisaEleitoral_v2.DTOs.IntencoesDeVoto
{
    public class IntencaoDeVotoResponseDTO
    {
        public int IntencaoDeVotoId { get; set; }
        public EleitorResponseDTO Eleitor { get; set; } = null!;
        public CandidatoResponseDTO Candidato { get; set; } = null!;
        public PesquisaResponseDTO Pesquisa { get; set; } = null!;
        public DateTime DataRegistro { get; set; }
    }
}
