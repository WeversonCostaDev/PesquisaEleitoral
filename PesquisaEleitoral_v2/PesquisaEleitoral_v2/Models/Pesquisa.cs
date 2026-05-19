using PesquisaEleitoral_v2.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.Models
{
    public class Pesquisa
    {
        public int PesquisaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Localidade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public Cargo Cargo { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        public ICollection<Candidato> Candidatos { get; set; } = [];
        public ICollection<IntencaoDeVoto> Votos { get; set; } = [];
    }
}
