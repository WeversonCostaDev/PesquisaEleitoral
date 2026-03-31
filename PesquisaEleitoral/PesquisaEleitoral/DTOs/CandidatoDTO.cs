using PesquisaEleitoral.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PesquisaEleitoral.DTOs
{
    public class CandidatoDTO
    {
        [Required]
        [StringLength(40)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(6)]
        public string? Partido { get; set; }

        [Required]
        [Range(10, 99)]
        public int Numero { get; set; }
    }
}
