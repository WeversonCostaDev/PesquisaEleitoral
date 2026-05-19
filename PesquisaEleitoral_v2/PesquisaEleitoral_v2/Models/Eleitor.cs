using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral_v2.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.Models
{
    public class Eleitor
    {
        public int EleitorId { get; set; }
        [Required]
        [StringLength(40)]
        public string Nome { get; set; } = string.Empty;
        [Precision(10,2)]
        public decimal Renda { get; set; }
        [Required]
        [Range(16,120)]
        public int Idade { get; set; }
        [Required]
        public Sexo Sexo { get; set; }
        [Required]
        public Regiao Regiao { get; set; }
        public Escolaridade Escolaridade { get; set; }
        public ICollection<IntencaoDeVoto> IntencoesDeVoto { get; set; } = [];
    }
}
