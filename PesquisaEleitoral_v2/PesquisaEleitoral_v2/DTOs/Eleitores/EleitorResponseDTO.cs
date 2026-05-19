using PesquisaEleitoral_v2.Enums;
using PesquisaEleitoral_v2.Models;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Eleitores
{
    public class EleitorResponseDTO
    {
        public int EleitorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Renda { get; set; }
        public int Idade { get; set; }
        public Sexo Sexo { get; set; }
        public Regiao Regiao { get; set; }
        public Escolaridade Escolaridade { get; set; }
    }
}
