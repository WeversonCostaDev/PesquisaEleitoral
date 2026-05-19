using PesquisaEleitoral_v2.Enums;
using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Pesquisas
{
    public class PesquisaDTO
    {
        [Required(ErrorMessage="Informe o nome.")]
        [StringLength(100, ErrorMessage ="Informe no máximo até 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "Informe a localidade.")]
        public string Localidade { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Informe no máximo até 300 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o cargo")]
        public Cargo Cargo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
