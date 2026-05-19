using PesquisaEleitoral_v2.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Eleitores
{
    public class EleitorUpdateDTO
    {
        [Required(ErrorMessage = "Informe o Id.")]
        public int EleitorId { get; set; }

        [Required(ErrorMessage = "Informe o Nome.")]
        [StringLength(40, ErrorMessage = "Informe no máximo 40 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a renda.")]
        public decimal Renda { get; set; }

        [Required(ErrorMessage = "Informe a idade.")]
        [Range(16, 120, ErrorMessage = "A idade deve estar entre 16 a 120 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Informe a o Sexo.")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "Informe a região.")]
        public Regiao Regiao { get; set; }

        [Required(ErrorMessage = "Informe a escolaridade.")]
        public Escolaridade Escolaridade { get; set; }
    }
}
