using PesquisaEleitoral.Enums;
using PesquisaEleitoral.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PesquisaEleitoral.DTOs.Eleitores
{
    public class EleitorDTO
    {
        [Required(ErrorMessage = "O nome do eleitor é obrigatório")]
        [StringLength(40, ErrorMessage = "O nome deve conter até 40 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "A idade é obrigatória")]
        [Range(16, 120, ErrorMessage = "A idade deve estar no intervalor de 16 a 120 anos.")]
        public int Idade { get; set; }

        [Required (ErrorMessage = "Informar o sexo é obrigatório.")]
        public Sexo Sexo { get; set; }

        [Required (ErrorMessage = "Informe a região.")]
        public Regiao Regiao { get; set; }
    }
}
