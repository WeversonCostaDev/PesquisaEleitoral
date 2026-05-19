using PesquisaEleitoral_v2.DTOs.Candidatos;
using PesquisaEleitoral_v2.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Pesquisas
{
    public class PesquisaResponseDTO
    {
        public int PesquisaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Cargo Cargo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public IEnumerable<CandidatoResponseDTO> Candidatos { get; set; } = Enumerable.Empty<CandidatoResponseDTO>();
    }
}
