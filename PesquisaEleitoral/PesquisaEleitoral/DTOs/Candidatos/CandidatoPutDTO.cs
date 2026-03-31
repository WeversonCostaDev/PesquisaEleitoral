using System.ComponentModel.DataAnnotations;

namespace PesquisaEleitoral.DTOs.Candidatos
{
    public class CandidatoPutDTO
    {
        [Required(ErrorMessage ="O Id é obrigatório")]
        public int CandidatoId { get; set; }

        [Required(ErrorMessage = "O nome do Candidato é obrigatório")]
        [StringLength(40, ErrorMessage = "O nome deve ter até 40 caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A sigla do partido é obrigatória")]
        [StringLength(10, ErrorMessage = "A sigla deve ter no máximo 10 caracteres")]
        public string? Partido { get; set; }

        [Required(ErrorMessage = "O número do partido é obrigatório")]
        [Range(10, 99, ErrorMessage = "O número deve estar entre 10 e 99")]
        public int Numero { get; set; }
    }
}
