using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral_v2.DTOs.Candidatos
{
    public class CandidatoUpdateDTO
    {
        [Required(ErrorMessage = "Informe o id.")]
        public int CandidatoId { get; set; }

        [Required(ErrorMessage = "Informe o Nome.")]
        [StringLength(40, ErrorMessage = "Informe no máximo 40 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Informe o partido.")]
        [StringLength(6, ErrorMessage = "Informe no máximo 6 letras")]
        public string Partido { get; set; } = null!;

        [Required(ErrorMessage = "Informe o número")]
        public int Numero { get; set; }
    }
}
