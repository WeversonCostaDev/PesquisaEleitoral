using PesquisaEleitoral.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs.Eleitores
{
    public class EleitorResponseDTO
    {
        public int EleitorId { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public Sexo Sexo { get; set; }
        public Regiao Regiao { get; set; }
    }
}
